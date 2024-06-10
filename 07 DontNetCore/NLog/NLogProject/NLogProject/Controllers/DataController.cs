using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NLog;
using NLog.Config;

namespace NLogProject.Controllers
{
    [ApiController]
    [Route("api/data")]
    public class DataController : Controller
    {
        private readonly NLog.ILogger _logger;

        public DataController()
        {
            _logger = LogManager.GetCurrentClassLogger();
        }

        [HttpGet("get")]
        public IActionResult Get(int userID)
        {
            //LoggingConfiguration config = LogManager.Configuration;

            //if (config.Variables.ContainsKey("userID"))
            //{
            //    config.Variables["userID"] = userID.ToString();
            //}

            //// Reconfigure NLog with the updated configuration
            //LogManager.ReconfigExistingLoggers();

            //_logger.Info("Entered");
            var logEvent = new LogEventInfo(NLog.LogLevel.Info, "userfileusinglogevent", "custom Log message");
            //logEvent.Properties["userid"] = userID.ToString();

            _logger.Log(logEvent);
            return Ok("Hello! from NLogProject");
        }
    }
}
