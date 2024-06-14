using Microsoft.AspNetCore.Builder;

namespace ExpenseSplittingApplication
{
    /// <summary>
    /// Main entry point class for the ExpenseSplittingApplication.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Main method that configures and starts the ASP.NET Core application.
        /// </summary>
        /// <param name="args">Command-line arguments passed to the application.</param>
        public static void Main(string[] args)
        {
            // Create a new instance of WebApplicationBuilder
            WebApplicationBuilder builder = WebApplication.CreateBuilder();

            // Create a new instance of Startup with configuration
            Startup startup = new Startup(builder.Configuration);

            // Configure services required by the application
            startup.ConfigureServices(builder.Services);

            // Build the WebApplication instance
            WebApplication app = builder.Build();

            // Configure the application using middleware and routing
            startup.Configure(app);

            // Run the application
            app.Run();
        }
    }
}
