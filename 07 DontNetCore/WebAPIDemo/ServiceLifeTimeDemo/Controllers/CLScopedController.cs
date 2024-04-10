using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceLifeTimeDemo.BL.Interface;
using ServiceLifeTimeDemo.BL.Service;
using System.Runtime.CompilerServices;

namespace ServiceLifeTimeDemo.Controllers
{
    //[Route("api/[controller]")]
    [ApiController]
    public class CLScopedController : ControllerBase
    {
        private IScopedService _scopedService;
        public CLScopedController(IScopedService scopedService)
        {
            _scopedService = scopedService;
        }

        [HttpGet]
        [Route("api/scoped")]
        public IActionResult Get([FromServices]IServiceProvider sp)
        {
            IScopedService scopedService2 = sp.GetService<IScopedService>();

            return Ok(new { scopedService1_Guid = _scopedService.GUID.ToString(), scopedService2_Guid = scopedService2.GUID.ToString() });
        }
    }
}
