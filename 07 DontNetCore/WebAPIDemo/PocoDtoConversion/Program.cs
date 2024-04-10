namespace PocoDtoConversion
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //RouteHandler rh = new RouteHandler(null);
            WebApplicationBuilder builder = WebApplication.CreateBuilder();

            Startup startup = new Startup();
            
            startup.ConfigureServices(builder.Services);

            WebApplication app = builder.Build();

            startup.Configure(app);
            //((IApplicationBuilder)app).Build();
            app.Run();
            //RouteHandler
        }
    }
}