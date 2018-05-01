using FluentMigrator.Runner;
using FluentMigrator.Runner.Announcers;
using FluentMigrator.Runner.Initialization;
using FluentMigrator.Runner.Processors;
using System;
using System.Data.SqlClient;
using FluentMigrator;
using FluentMigrator.Runner.Processors.SqlServer;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Threading;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace todo_db
{
    class Program
    {
        private static ServiceProvider _provider;
        private static IConfigurationRoot _config;
        private static ILogger<Program> _logger;

        static void Main(string[] args)
        {
            _config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", false, true)
                .AddEnvironmentVariables()
                .Build();

            _provider = new ServiceCollection()
                .AddSingleton<CancellationTokenSource>()
                .AddOptions()
                .Configure<DatabaseOptions>(_config.GetSection("database"))
                .AddLogging(cfg =>
                {
                    cfg.AddConfiguration(_config);
                    cfg.AddConsole();
                })
                .BuildServiceProvider();

            _logger = _provider.GetRequiredService<ILogger<Program>>();

            IOptionsSnapshot<DatabaseOptions> dbSettings = _provider.GetService<IOptionsSnapshot<DatabaseOptions>>();
            CreateDatabaseIfNotExists(dbSettings);

            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
            builder.UserID = dbSettings.Value.User;
            builder.Password = dbSettings.Value.Password;
            builder.DataSource = dbSettings.Value.Server;
            if (dbSettings.Value.Name != null)
            {
                builder.InitialCatalog = dbSettings.Value.Name;
            }

            _logger.LogInformation("Using connection string {0} for migrations.", builder);


            // Create the announcer to output the migration messages
            ConsoleAnnouncer announcer = new ConsoleAnnouncer()
            {
                ShowSql = true,
                ShowElapsedTime = true
            };

            // Processor specific options (usually none are needed)
            ProcessorOptions options = new ProcessorOptions();

            // Initialize the DB-specific processor
            SqlServerProcessorFactory processorFactory = new SqlServerProcessorFactory();
            IMigrationProcessor processor = processorFactory.Create(builder.ToString(), announcer, options);

            // Configure the runner
            RunnerContext context = new RunnerContext(announcer)
            {
                AllowBreakingChange = true,
            };

            // Create the migration runner
            MigrationRunner runner = new MigrationRunner(
                typeof(Program).Assembly,
                context,
                processor);

            // Run the migrations
            runner.MigrateUp();
            Console.WriteLine("Hello World!");
        }

        private static void CreateDatabaseIfNotExists(IOptionsSnapshot<DatabaseOptions> dbSettings)
        {
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
            builder.UserID = dbSettings.Value.User;
            builder.Password = dbSettings.Value.Password;
            builder.DataSource = dbSettings.Value.Server;

            using (SqlConnection connection = new SqlConnection(builder.ToString()))
            {
                connection.Open();
                SqlCommand command = connection.CreateCommand();
                command.CommandText = @"if not exists(select * from sys.databases where name = 'todo')
                                        CREATE DATABASE todo";
                command.ExecuteNonQuery();
                connection.Close();
                _logger.LogInformation("Created todo database.");
            }
        }
    }
}
