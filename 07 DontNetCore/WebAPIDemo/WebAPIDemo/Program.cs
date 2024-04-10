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

            //app.MapControllers();
            // run the web app
            app.Run();

            //RequestCustom(builder.Services);

        }

        public static async void RequestCustom(IServiceCollection services)
        {
            var ctx = new DefaultHttpContext();

            // setup a DI scope for scoped services in your controllers etc.
            using var scope = new DefaultServiceProviderFactory().CreateServiceProvider(services).CreateScope();
            ctx.RequestServices = scope.ServiceProvider;

            // prepare the request as needed
            ctx.Request.Body = new MemoryStream(new byte[] { 104, 101, 108, 108, 111 });
            ctx.Request.ContentType = "application/json";
            ctx.Request.ContentLength = 5;
            ctx.Request.Method = "GET";
            ctx.Request.Path = PathString.FromUriComponent("/weather");

            // you only need this if you are hosting in IIS (.UseIISIntegration())
            ctx.Request.Headers["MS-ASPNETCORE-TOKEN"] = Environment.GetEnvironmentVariable("ASPNETCORE_TOKEN");

            // setup a place to hold the response body
            //ctx.Response.Body = new MemoryStream();

            // execute the request
            await Global.rd(ctx);

            // interpret the result as needed, e.g. parse the body
            ctx.Response.Body.Seek(0, SeekOrigin.Begin);
            using var reader = new StreamReader(ctx.Response.Body);
            string body = await reader.ReadToEndAsync();
        }
    }

}