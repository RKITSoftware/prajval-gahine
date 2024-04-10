using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceLifeTimeDemo.BL.Interface;
using ServiceLifeTimeDemo.BL.Service;
using System.Runtime.CompilerServices;

namespace ServiceLifeTimeDemo.Controllers
{
    //[Route("api/[controller]")]
    [ApiController]
    public class CLTransientController : ControllerBase
    {
        private ITransientService _transientService;
        public CLTransientController(ITransientService transientService)
        {
            _transientService = transientService;
        }

        [HttpGet]
        [Route("api/transient")]
        public IActionResult Get([FromServices]IServiceProvider sp)
        {
            throw new NotImplementedException("Not impl ex generated");
            ITransientService transientService2 = sp.GetService<ITransientService>();

            return Ok(new { transientService1_Guid = _transientService.GUID.ToString(), transientService2_Guid = transientService2.GUID.ToString() });
        }
    }
}
