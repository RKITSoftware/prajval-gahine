using Microsoft.AspNetCore.Builder;

namespace ExpenseSplittingApplication
{
    public class Program
    {
        public static void Main(string[] args)
        {

            WebApplicationBuilder builder = WebApplication.CreateBuilder();

            Startup startup = new Startup(builder.Configuration);

            startup.ConfigureServices(builder.Services);

            WebApplication app = builder.Build();

            startup.Configure(app);

            app.Run();
        }
    }
}
