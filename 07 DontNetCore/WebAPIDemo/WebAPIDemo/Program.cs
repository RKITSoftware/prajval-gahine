using Microsoft.AspNetCore.Hosting;
using System.Formats.Asn1;
using WebAPIDemo.Constants;

namespace WebAPIDemo
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // create web application builder
            WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

            // create startup instance
            IStartup startup = new Startup();
            startup.ConfigureServices(builder.Services);

            // builder app
            WebApplication app = builder.Build();

            // configure app
            startup.Configure(app);

            //Console.ReadLine();

            // run the web app

            //app.Run();

            RequestCustom(builder.Services);

        }

        /// <summary>
        /// A method to simulate a request to this application
        /// </summary>
        /// <param name="services">A service provider</param>
        public static async void RequestCustom(IServiceCollection services)
        {
            // setup a DI scope for scoped services in your controllers etc.
            using var scope = new DefaultServiceProviderFactory().CreateServiceProvider(services).CreateScope();

            // creating a http context
            var ctx = new DefaultHttpContext();
            ctx.RequestServices = scope.ServiceProvider;

            // prepare the request as needed
            ctx.Request.Body = new MemoryStream(new byte[] { 104, 101, 108, 108, 111 });
            ctx.Request.ContentType = "application/json";
            ctx.Request.ContentLength = 5;
            ctx.Request.Method = "GET";
            //ctx.Request.Path = PathString.FromUriComponent("/WeatherForecast/Get2");
            ctx.Request.Path = PathString.FromUriComponent("/WeatherForecast/Get");
            ctx.Request.QueryString = QueryString.FromUriComponent("?parameter1=1&parameter2=1");

            // you only need this if you are hosting in IIS (.UseIISIntegration())
            ctx.Request.Headers["MS-ASPNETCORE-TOKEN"] = Environment.GetEnvironmentVariable("ASPNETCORE_TOKEN");

            // setup a place to hold the response body
            ctx.Response.Body = new MemoryStream();

            // execute the request
            await Global.rd(ctx);

            // interpret the result as needed, e.g. parse the body
            ctx.Response.Body.Seek(0, SeekOrigin.Begin);
            using var reader = new StreamReader(ctx.Response.Body);
            string body = await reader.ReadToEndAsync();
        }
    }
}