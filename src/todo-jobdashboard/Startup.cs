using System.Data.SqlClient;
using Hangfire;
using Hangfire.Dashboard;
using Hangfire.SqlServer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

// ReSharper disable UnusedMember.Global

namespace Todo.JobDashboard
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<HangfireOptions>(Configuration.GetSection("hangfire"));
            services.AddTransient<ConfigureHangfireService>();

            services.AddHangfire(a => { });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            ConfigureHangfireService hangfire = app.ApplicationServices.GetService<ConfigureHangfireService>();
            app.UseHangfireDashboard(
                options: new DashboardOptions(){ 
                    Authorization = new [] { new MyAuthorizationFilter() }
                },
                storage: new SqlServerStorage(hangfire.GetConnectionString()));
        }
    }

    public class MyAuthorizationFilter : IDashboardAuthorizationFilter
    {
        public bool Authorize(DashboardContext context)
        {
            return true;
        }
    }

    public class ConfigureHangfireService
    {
        private readonly HangfireOptions _options;

        public ConfigureHangfireService(IOptions<HangfireOptions> options)
        {
            _options = options.Value;
        }

        public string GetConnectionString()
        {            
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
            builder.DataSource = _options.Server;
            builder.UserID = _options.User;
            builder.Password = _options.Password;

            if (!string.IsNullOrEmpty(_options.Name))
            {
                builder.InitialCatalog = _options.Name;
            }

            return builder.ToString();
        }
    }

    public class HangfireOptions
    {
        public string User { get; set; }
        public string Password { get; set; }
        public string Server { get; set; }
        public string Name { get; set; }
    }
}
