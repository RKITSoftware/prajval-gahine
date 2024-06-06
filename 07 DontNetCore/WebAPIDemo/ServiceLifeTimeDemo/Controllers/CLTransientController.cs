using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceLifeTimeDemo.BL.Interface;
using ServiceLifeTimeDemo.BL.Service;
using System.Runtime.CompilerServices;

namespace ServiceLifeTimeDemo.Controllers
{
    /// <summary>
    /// CLTransientController class
    /// </summary>
    [ApiController]
    public class CLTransientController : ControllerBase
    {
        /// <summary>
        /// ITransientService instance
        /// </summary>
        private ITransientService _transientService;

        /// <summary>
        /// CLTransientController constructor to initialize ITransientService
        /// </summary>
        /// <param name="scopedService">ITransientService insatnce</param>
        public CLTransientController(ITransientService transientService)
        {
            _transientService = transientService;
        }

        /// <summary>
        /// Method to get guid of transient service
        /// </summary>
        /// <returns>object containing guid info</returns>
        [HttpGet]
        [Route("api/transient")]
        public IActionResult Get([FromServices]IServiceProvider sp)
        {
            //throw new NotImplementedException("Not impl ex generated");
            ITransientService transientService2 = sp.GetService<ITransientService>();

            return Ok(new { transientService1_Guid = _transientService.GUID.ToString(), transientService2_Guid = transientService2.GUID.ToString() });
        }
    }
}
