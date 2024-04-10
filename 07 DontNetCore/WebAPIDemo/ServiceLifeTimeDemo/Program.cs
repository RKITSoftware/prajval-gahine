using ModelAndBLClassLib.BL;
using Microsoft.AspNetCore.Diagnostics.RazorViews;

namespace ServiceLifeTimeDemo
{
    public class Program
    {
        public static void Main(string[] args)
        {

            IConfiguration config = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .AddEnvironmentVariables()
            .Build();


            var builder = Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
            builder.Build().Run();

            //= new WebHostBuilder();
            //builder.UseKestrel()
            //    .UseConfiguration(config)
            //    .UseStartup<Startup>();
            //IWebHost app = builder.Build();
            //app.Run();
            //ApiBehaviorApplicationModelProvider
            //Microsoft.AspNetCore.Diagnostics.ViewsErrorPage
        }
    }
}