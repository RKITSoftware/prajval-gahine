using NLog;
using NLog.Config;
using NLog.Targets;

namespace NLogInConsoleApp
{
    internal class LoggingWithoutConfigFile
    {
        public static void Run()
        {
            // nlog config - targets, rules, layouts, filters
            LoggingConfiguration nLogConfig = new LoggingConfiguration();

            // file target
            FileTarget fileTarget = new FileTarget()
            {
                FileName = "logWithoutConfig/mylog.log",
                Layout = "${longdate} ${level:uppercase=true} ${callsite:className=true:methodName=true} ${callsite-linenumber} ${message} ${exception}",
                Name = "file378",
            };

            // console target
            var consoleTarget = new ConsoleTarget("console")
            {
                Layout = "${longdate} ${level:uppercase=true} ${callsite:className=true:methodName=true} ${callsite-linenumber} ${message} ${exception}"
            };

            nLogConfig.AddTarget(fileTarget);
            nLogConfig.AddTarget(consoleTarget);

            // add rules to target
            nLogConfig.AddRule(LogLevel.Trace, LogLevel.Fatal, fileTarget);
            nLogConfig.AddRule(LogLevel.Trace, LogLevel.Fatal, consoleTarget);

            // apply config
            LogManager.Configuration = nLogConfig;

            Logger logger = LogManager.GetCurrentClassLogger();

            logger.Info("This is an info message");
        }
    }
}
