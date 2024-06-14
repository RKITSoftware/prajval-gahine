using ExpenseSplittingApplication.BL.Common.Interface;
using NLog;

namespace ExpenseSplittingApplication.BL.Common.Service
{
    /// <summary>
    /// Service class for logging user-related events using NLog.
    /// </summary>
    public class UserLoggerService : ILoggerService
    {
        /// <summary>
        /// NLog logger instance for logging messages.
        /// </summary>
        private readonly Logger _logger;

        /// <summary>
        /// The ID of the user associated with the logger.
        /// </summary>
        private readonly string _userId;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserLoggerService"/> class.
        /// </summary>
        /// <param name="userId">The ID of the user associated with the logger.</param>
        public UserLoggerService(string userId)
        {
            _logger = LogManager.GetCurrentClassLogger();
            _userId = userId;
        }

        /// <summary>
        /// Logs an error message with the associated user ID.
        /// </summary>
        /// <param name="message">The error message to log.</param>
        public void Error(string message)
        {
            LogEventInfo logEvent = new LogEventInfo(LogLevel.Error, _logger.Name, message);
            logEvent.Properties["userId"] = _userId;
            _logger.Log(logEvent);
        }

        /// <summary>
        /// Logs an information message with the associated user ID.
        /// </summary>
        /// <param name="message">The information message to log.</param>
        public void Information(string message)
        {
            LogEventInfo logEvent = new LogEventInfo(LogLevel.Info, _logger.Name, message);
            logEvent.Properties["userId"] = _userId;
            _logger.Log(logEvent);
        }

        /// <summary>
        /// Logs a warning message with the associated user ID.
        /// </summary>
        /// <param name="message">The warning message to log.</param>
        public void Warning(string message)
        {
            LogEventInfo logEvent = new LogEventInfo(LogLevel.Warn, _logger.Name, message);
            logEvent.Properties["userId"] = _userId;
            _logger.Log(logEvent);
        }
    }
}
