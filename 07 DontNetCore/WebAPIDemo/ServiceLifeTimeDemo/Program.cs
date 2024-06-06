using ModelAndBLClassLib.BL;
using Microsoft.AspNetCore.Diagnostics.RazorViews;

namespace ServiceLifeTimeDemo
{
    public class Program
    {
        public static void Main(string[] args)
        {
            /*
            var builder = Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });

            builder.Build().Run();
            */

            IConfiguration config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .AddEnvironmentVariables()
                .Build();

            IWebHostBuilder builder = new WebHostBuilder();
            builder.UseKestrel()
                .UseConfiguration(config)
                .UseStartup<Startup>();
            IWebHost app = builder.Build();
            app.Run();
        }
    }
}


//ApiBehaviorApplicationModelProvider
//Microsoft.AspNetCore.Diagnostics.ViewsErrorPage