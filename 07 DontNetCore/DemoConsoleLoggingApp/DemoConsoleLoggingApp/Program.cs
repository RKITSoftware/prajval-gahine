using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace DemoConsoleLoggingApp
{
    internal class Program
    {
        static void Main(string[] args)
        {

            IConfigurationBuilder builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables();

            // Build the configuration
            IConfiguration configuration = builder.Build();


            ILoggerFactory loggerFactory = LoggerFactory.Create(builder =>
            {
                builder
                    .AddConsole()
                    .AddDebug()
                    .AddConfiguration(configuration.GetSection("Logging"));
            });

            ILogger logger = loggerFactory.CreateLogger<Program>();

            logger.LogDebug("Debug");
            logger.LogTrace("Trace");
            logger.LogCritical("Critical");
            logger.LogError("Error");
            logger.LogWarning("Warning");
            logger.LogInformation("Information");

            Console.WriteLine(configuration.GetValue<string>("name"));
        }
    }

    public class final
    {
        public static void Main()
        {
            var test1 = new Test();
            var test2 = new Test<int>();
        }
    }

    internal class Test
    {
        public Test()
        {

        }
    }
    internal class Test<T>
    {
        public Test()
        {
        }
    }
}
