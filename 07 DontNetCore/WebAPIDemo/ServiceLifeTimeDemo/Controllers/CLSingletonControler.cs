using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceLifeTimeDemo.BL.Interface;
using System.Net;
using System.Runtime.CompilerServices;

namespace ServiceLifeTimeDemo.Controllers
{
    //[Route("api/[controller]")]
    [ApiController]
    public class CLSingletonController : ControllerBase
    {
        private ISingletonService _singletonService;
        public CLSingletonController(ISingletonService singletonService)
        {
            _singletonService = singletonService;
        }

        [HttpGet]
        [Route("api/singleton")]
        public IActionResult Get()
        {
            throw new BadHttpRequestException("bad request PNG", 400);
            return Ok(_singletonService.GUID.ToString());
        }
    }
}
