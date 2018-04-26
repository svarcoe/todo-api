using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Swagger;

namespace todo_api
{
    public class Startup
    {
	    private readonly ILogger<Startup> _log;

	    public Startup(IConfiguration configuration, ILoggerFactory loggerFactory)
	    {
		    _log = loggerFactory.CreateLogger<Startup>();
		    Configuration = configuration;
	    }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddSwaggerGen(c=>{
				c.SwaggerDoc("v1", new Info { Title = "Todo API", Version = "v1" });
            });
	        _log.LogInformation("Done configuring services");
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseHttpsRedirection();
            EnableSwagger(app);
	        app.UseMiddleware<ErrorHandlingMiddleware>();
	        app.UseMvc();
			_log.LogInformation("Done configuring application builder.");
        }

	    private void EnableSwagger(IApplicationBuilder app)
	    {
		    app.UseSwagger();
		    app.UseSwaggerUI(cfg =>
		    {
			    cfg.SwaggerEndpoint("/swagger/v1/swagger.json", "Todo API v1");
		    });
			_log.LogDebug("Swagger enabled.");
	    }
    }
}