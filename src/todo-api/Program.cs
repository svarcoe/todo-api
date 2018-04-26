using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using NLog.Web;

namespace todo_api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
	            .ConfigureLogging((hostingContext, logging) =>
	            {
		            logging.ClearProviders();
		            logging.SetMinimumLevel(LogLevel.Trace);
		            logging.AddConfiguration(hostingContext.Configuration.GetSection("Logging"));
		            logging.AddConsole();
		            logging.AddDebug();
	            })
                .UseNLog()
                .Build();
    }
}
