namespace LoggingDemo
{
    public class Program
    {
        public static void Main(string[] args)
        {
            using var loggerFactory = LoggerFactory.Create(static builder =>
            {
                builder.ClearProviders();
                builder
                    .AddConsole();
            });

            ILogger logger = loggerFactory.CreateLogger<Program>();
            logger.LogDebug("Hello {Target}", "Everyone");
            logger.LogInformation("Hello, World");
            logger.LogError("Hello, World");
            logger.LogWarning("Hello, World");
            logger.LogTrace("Hello, World");
        }
    }
}