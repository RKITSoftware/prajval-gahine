using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using NLog;
using NLog.Web;

namespace NLogInConsoleApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // create a configuration instance
            IConfiguration configuration = new ConfigurationBuilder()
                .AddJsonFile("appsetting.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables()
                .Build();

            //LogFactory logFactory = NLogBuilder.ConfigureNLog("nlog.config");
            LogFactory logFactory = LogManager.Setup().RegisterNLogWeb().LoadConfigurationFromFile("nlog.config", optional: true).LogFactory;
            Logger logger = logFactory.GetLogger("Program");

            logger.Trace("Trace");
            logger.Debug("Debug");
            logger.Info("Info");
            logger.Warn("Warn");
            logger.Error("Error");
            logger.Fatal("Fatal");
        }
    }
}