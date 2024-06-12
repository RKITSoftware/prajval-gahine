using ExpenseSplittingApplication.BL.Common.Interface;
using NLog;

namespace ExpenseSplittingApplication.BL.Common.Service
{
    public class UserLoggingService : ILoggerService
    {

        private readonly Logger _logger;

        private readonly string _userId;

        public UserLoggingService(string userId)
        {
            _logger = LogManager.GetCurrentClassLogger();
            _userId = userId;
        }

        public void Error(string message)
        {
            LogEventInfo logEvent = new LogEventInfo(LogLevel.Error, _logger.Name, message);
            logEvent.Properties["userId"] = _userId;
            _logger.Log(logEvent);
        }

        public void Information(string message)
        {
            LogEventInfo logEvent = new LogEventInfo(LogLevel.Info, _logger.Name, message);
            logEvent.Properties["userId"] = _userId;
            _logger.Log(logEvent);
        }

        public void Warning(string message)
        {
            LogEventInfo logEvent = new LogEventInfo(LogLevel.Warn, _logger.Name, message);
            logEvent.Properties["userIds"] = _userId;
            _logger.Log(logEvent);
        }
    }
}
