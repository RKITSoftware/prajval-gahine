using Microsoft.AspNetCore.Mvc.ActionConstraints;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using System.Reflection;

namespace ControllerInitialization
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
            //((IApplicationBuilder)app).Build();
            app.Run();
            //RequestDelegate rq = ((IApplicationBuilder)app).Build();
            //rq(null);
            //RouteHandler
            //DefaultControllerFactory
            //DefaultControllerActivator
            //IControllerFactory
            //ServiceProvider
            //RuntimeTypeHandle
            //CustomAttributeData
            //ControllerActionInvoker
            //ActionSelector
            //ControllerActionInvokerCacheEntry
            //ServiceProvider
        }
    }
}