using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceLifeTimeDemo.BL.Interface;
using ServiceLifeTimeDemo.BL.Service;
using System.Runtime.CompilerServices;

namespace ServiceLifeTimeDemo.Controllers
{
    /// <summary>
    /// CLScopedController class
    /// </summary>
    [ApiController]
    public class CLScopedController : ControllerBase
    {
        /// <summary>
        /// Scoped Service instance
        /// </summary>
        private IScopedService _scopedService;

        /// <summary>
        /// CLScopedController constructor to initialize IScopedService
        /// </summary>
        /// <param name="scopedService">IScopedService instance</param>
        public CLScopedController(IScopedService scopedService)
        {
            _scopedService = scopedService;
        }

        /// <summary>
        /// Method to get guid of scoped service
        /// </summary>
        /// <param name="sp">IServiceProvider instance</param>
        /// <returns>object containing guid information</returns>
        [HttpGet]
        [Route("api/scoped")]
        public IActionResult Get([FromServices]IServiceProvider sp)
        {
            IScopedService scopedService2 = sp.GetService<IScopedService>();

            return Ok(new { scopedService1_Guid = _scopedService.GUID.ToString(), scopedService2_Guid = scopedService2.GUID.ToString() });
        }
    }
}
