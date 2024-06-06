using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceLifeTimeDemo.BL.Interface;
using System.Net;
using System.Runtime.CompilerServices;

namespace ServiceLifeTimeDemo.Controllers
{
    /// <summary>
    /// CLSingletonController class
    /// </summary>
    [ApiController]
    public class CLSingletonController : ControllerBase
    {
        /// <summary>
        /// ISingletonService instance
        /// </summary>
        private ISingletonService _singletonService;

        /// <summary>
        /// CLSingletonController constructor to initialize ISingletonService
        /// </summary>
        /// <param name="scopedService">ISingletonService insatnce</param>
        public CLSingletonController(ISingletonService singletonService)
        {
            _singletonService = singletonService;
        }

        /// <summary>
        /// Method to get guid of singleton service
        /// </summary>
        /// <returns>object containing guid</returns>
        [HttpGet]
        [Route("api/singleton")]
        public IActionResult Get()
        {
            //throw new BadHttpRequestException("bad request PNG", 400);
            return Ok(_singletonService.GUID.ToString());
        }
    }
}
