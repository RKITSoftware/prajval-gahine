using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LoggingDemo.Controllers
{
    /// <summary>
    /// Data controller
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class DataController : ControllerBase
    {
        /// <summary>
        /// Method to get data
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("")]
        public IActionResult Get()
        {
            return Ok("Done");
        }
    }
}
