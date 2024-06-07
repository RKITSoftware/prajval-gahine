using NLog.Web;
using NLog;
using NLog.LayoutRenderers;
using NLogProject.Extensions.Services;
using System.Reflection.PortableExecutable;
using System;

namespace NLogProject
{
    public class Program
    {
        public static void Main(string[] args)
        {
            WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

            Startup startup = new Startup();

            startup.ConfigureServices(builder.Services);

            WebApplication app = builder.Build();

            startup.Configure(app);
                
            // Nlog
            // NLog.LogFactory logFactory = NLogBuilder.ConfigureNLog("nlog.config");
            //LogManager.Setup().LoadConfigurationFromFile("nlog.config");

            NLog.ILogger logger = LogManager.GetCurrentClassLogger();

            //LayoutRenderer.Register<UserIdLayoutRenderer>("userID");
            LogManager.Setup().SetupExtensions(e =>
            {
                e.RegisterLayoutRenderer<UserIdLayoutRenderer>("userid");
                LogManager.Configuration = LogManager.Configuration.Reload();
                LogManager.ReconfigExistingLoggers();
            });

            try
            {
                logger.Info("Web application started!");
                app.Run();
            }
            finally
            {
                LogManager.Shutdown();
            }
        }
    }
}