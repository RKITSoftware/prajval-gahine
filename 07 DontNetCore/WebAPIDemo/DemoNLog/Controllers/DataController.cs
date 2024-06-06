using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DemoNLog.Controllers
{
    /// <summary>
    /// Data controller class
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class DataController : ControllerBase
    {
        /// <summary>
        /// Ilogger instance
        /// </summary>
        private ILogger<DataController> _logger;

        /// <summary>
        /// DataController Constructor
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="loggerProgram"></param>
        public DataController(ILogger<DataController> logger, ILogger<Program> loggerProgram)
        {
            if(logger == loggerProgram)
            {
                Console.WriteLine("Yes they r same objects");                      
            }

            Console.WriteLine(logger.GetHashCode());

            Console.WriteLine(loggerProgram.GetHashCode());
            _logger = logger;
        }

        /// <summary>
        /// Action method to get some data
        /// </summary>
        /// <returns></returns>
        [HttpGet("")]
        public IActionResult Get()
        {
            _logger.LogError("Into Get method");
            _logger.LogError("Exiting Get method");
            _logger.LogError("This is an error");
            return Ok("Logging done");
        }
    }
}
