using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    /// <summary>
    /// Controller to handle attendance related apis/endpoints
    /// </summary>
    [Route("api/attendance")]
    public class AttendanceController : ApiController
    {
        [HttpGet]
        [Route("get-attedances")]
        public IHttpActionResult GetAttendances()
        {
            List<Attendance> attendances = Attendance.GetAttendances();

            //return Attendance.GetAttendances();
            return Ok(attendances);
        }
    }
}
