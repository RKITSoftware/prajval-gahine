using Microsoft.AspNetCore.Mvc;

namespace ExceptionCoreDemo.Controllers
{
    /// <summary>
    /// Data controller
    /// </summary>
    public class CLDataController
    {
        /// <summary>
        /// Action method to get data
        /// </summary>
        /// <returns>IActionResult instance</returns>
        /// <exception cref="NotImplementedException">NotImplementedException excetpion</exception>
        [HttpGet]
        [Route("api/data")]
        public IActionResult Get()
        {
            throw new NotImplementedException();
        }
    }
}
