using NLog.Web;

namespace DemoNLog
{
    public class Program
    {
        public static void Main()
        {
            IWebHostBuilder builder = new WebHostBuilder();

            IConfiguration configuration = new ConfigurationManager()
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddEnvironmentVariables()
                .Build();

            builder.UseKestrel()
                .UseStartup<Startup>()
                .UseNLog()
                .UseConfiguration(configuration);

            IWebHost app = builder.Build();

            app.Run();
        }
    }
}