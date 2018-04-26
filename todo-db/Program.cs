using FluentMigrator.Runner;
using FluentMigrator.Runner.Announcers;
using FluentMigrator.Runner.Initialization;
using FluentMigrator.Runner.Processors;
using FluentMigrator.Runner.Processors.SQLite;
using System;

namespace todo_db
{
    class Program
    {
        static void Main(string[] args)
		{

			// Create the announcer to output the migration messages
			ConsoleAnnouncer announcer = new ConsoleAnnouncer()
			{
				ShowSql = true,
			};

			// Processor specific options (usually none are needed)
			ProcessorOptions options = new ProcessorOptions();

			// Initialize the DB-specific processor
			SQLiteProcessorFactory processorFactory = new SQLiteProcessorFactory();
			FluentMigrator.IMigrationProcessor processor = processorFactory.Create(null, announcer, options);

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
    }
}
