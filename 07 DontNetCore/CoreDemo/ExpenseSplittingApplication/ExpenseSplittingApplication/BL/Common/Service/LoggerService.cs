using ExpenseSplittingApplication.BL.Common.Interface;
using NLog;
using System;

namespace ExpenseSplittingApplication.BL.Common.Service
{
    public class LoggerService : ILoggerService
    {

        private static Logger _logger = LogManager.GetCurrentClassLogger();

        public void Error(Exception ex)
        {
            throw new NotImplementedException();
        }

        public void Error(string message)
        {
            throw new NotImplementedException();
        }

        public void Information(string message)
        {
            throw new NotImplementedException();
        }

        public void Warning(string message)
        {
            throw new NotImplementedException();
        }
    }
}
