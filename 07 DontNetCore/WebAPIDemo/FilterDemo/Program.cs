
namespace FilterDemo
{
    public class Program
    {
        public static void Main(string[] args)
        {
            WebApplicationBuilder builder = WebApplication.CreateBuilder();

            Startup startup = new Startup();

            startup.ConfigureServices(builder.Services);

            WebApplication app = builder.Build();

            startup.Configure(app);

            app.Run();
        }
    }
}