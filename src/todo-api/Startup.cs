using IdentityServer4.AccessTokenValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Swagger;
using Todo.Api.Todos;
// ReSharper disable UnusedMember.Global

namespace Todo.Api
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
            
            services.AddAuthentication(IdentityServerAuthenticationDefaults.AuthenticationScheme)
                .AddIdentityServerAuthentication(options =>
                {
                    options.Authority = "http://auth:5000";
                    options.RequireHttpsMetadata = false;
                    options.ApiName = "api1";
                });
            services.AddCors();
            services.Configure<ServerOptions>(Configuration.GetSection("server"));
            services.Configure<DatabaseOptions>(Configuration.GetSection("database"));
            services.AddTransient<ITodoService, TodoService>();
            services.AddSingleton<ISqlConnectionFactory, SqlConnectionFactory>();
            services.AddSwaggerGen(c=>{
				c.SwaggerDoc("v1", new Info { Title = "Todo API", Version = "v1" });
            });
	        _log.LogInformation("Done configuring services");
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            ConfigureHttpsRedirection(app);
            
            EnableSwagger(app);
	        app.UseMiddleware<ErrorHandlingMiddleware>();
            app.UseCors(builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            app.UseAuthentication();
	        app.UseMvc();
			_log.LogInformation("Done configuring application builder.");
        }

        private void ConfigureHttpsRedirection(IApplicationBuilder app)
        {
            IOptions<ServerOptions> serverOptions = app.ApplicationServices.GetRequiredService<IOptions<ServerOptions>>();
            _log.LogDebug("HttpsRedirectionDisabled set to {0}", serverOptions.Value.HttpsRedirectionDisabled);
            if (!serverOptions.Value.HttpsRedirectionDisabled)
            {
                app.UseHttpsRedirection();
            }
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