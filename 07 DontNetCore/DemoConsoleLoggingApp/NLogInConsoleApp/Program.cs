using Microsoft.Extensions.Configuration;
using NLog;
using NLog.Web;

namespace NLogInConsoleApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            /*
            // create a configuration instance
            IConfiguration configuration = new ConfigurationBuilder()
                .AddJsonFile("appsetting.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables()
                .Build();

            //LogFactory logFactory = NLogBuilder.ConfigureNLog("nlog.config");
            LogFactory logFactory = LogManager.Setup().RegisterNLogWeb().LoadConfigurationFromFile("nlog.config", optional: true).LogFactory;
            Logger logger = logFactory.GetLogger("Program");


            var x = logger.Properties;

            logger.Trace("CM: Trace");
            logger.Debug("CM: Debug");
            logger.Info("CM: Info");
            logger.Warn("CM: Warn");
            logger.Error("CM: Error");
            logger.Fatal("CM: Fatal");
            */

            LoggingWithoutConfigFile.Run();
        }
    }
}