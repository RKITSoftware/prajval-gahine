using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DemoNLog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DataController : ControllerBase
    {
        private ILogger<DataController> _logger;
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

        [HttpGet("")]
        public IActionResult Get()
        {
            _logger.LogError("Into Get method");
            _logger.LogError("Exiting Get method");
            return Ok("Logging done");
        }
    }
}
