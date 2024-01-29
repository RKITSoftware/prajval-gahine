using System.Collections.Generic;
using System.Web.Http;
using WebApplication2.BL;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    /// <summary>
    /// Controller to handle attendance related apis/endpoints
    /// </summary>
    [Route("api/attendance")]
    public class CLAttendanceController : ApiController
    {
        [HttpGet]
        [Route("get-attedances")]
        public IHttpActionResult GetAttendances()
        {
            List<ATD01> lstAttendance = BLAttendance.GetAttendances();

            return Ok(lstAttendance);
        }
    }
}
