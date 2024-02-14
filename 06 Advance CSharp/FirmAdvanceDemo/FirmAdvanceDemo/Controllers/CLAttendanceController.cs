using FirmAdvanceDemo.BL;
using FirmAdvanceDemo.Models;
using FirmAdvanceDemo.Utitlity;
using Newtonsoft.Json.Linq;
using System;
using System.Drawing;
using System.Net.Http;
using System.Web.Http;

namespace FirmAdvanceDemo.Controllers
{
    [RoutePrefix("api/attendance")]
    public class CLAttendanceController : ApiController
    {
        /// <summary>
        /// Method used to have consistent (uniform) returns from all controller actions
        /// </summary>
        /// <param name="responseStatusInfo">ResponseStatusInfo instance containing response specific information</param>
        /// <returns>Instance of type IHttpActionResult</returns>
        [NonAction]
        public IHttpActionResult Returner(ResponseStatusInfo responseStatusInfo)
        {
            if (responseStatusInfo.IsRequestSuccessful)
            {
                return Ok(ResponseWrapper.Wrap(responseStatusInfo.Message, responseStatusInfo.Data));
            }
            return ResponseMessage(Request.CreateErrorResponse(responseStatusInfo.StatusCode, responseStatusInfo.Message));
        }

        /// <summary>
        /// Action method to get all attendances of employees'
        /// </summary>
        /// <returns>Instance of type IHttpActionResult</returns>
        [HttpGet]
        [Route("")]
        public IHttpActionResult GetAttendances()
        {
            ResponseStatusInfo responseStatusInfo = BLResource<ATD01>.FetchResource();
            return this.Returner(responseStatusInfo);
        }

        /// <summary>
        /// Action method to get an employee's attendances
        /// </summary>
        /// <param name="id">Employee Id</param>
        /// <returns>Instance of type IHttpActionResult</returns>
        [HttpGet]
        [Route("employee/{id}")]
        public IHttpActionResult GetAttendanceByEmployeeId(int id)
        {
            ResponseStatusInfo responseStatusInfo = BLAttendance.FetchAttendanceByEmployeeId(id);
            return this.Returner(responseStatusInfo);
        }

        /// <summary>
        /// Action method to get all employees attendances by month-year
        /// </summary>
        /// <param name="month">Attendance Month</param>
        /// <param name="year">Attendance Year</param>
        /// <returns>Instance of type IHttpActionResult</returns>
        [HttpGet]
        [Route("monthyear/{month}/{year}")]
        public IHttpActionResult GetAttendanceByMonthYear(int month, int year)
        {
            ResponseStatusInfo responseStatusInfo = BLAttendance.FetchAttendanceByMonthYear(month, year);
            return this.Returner(responseStatusInfo);
        }

        /// <summary>
        /// Action method to get today's attendances of all employees
        /// </summary>
        /// <returns>Instance of type IHttpActionResult</returns>
        [HttpGet]
        [Route("today")]
        public IHttpActionResult GetTodaysAttendance()
        {
            ResponseStatusInfo responseStatusInfo = BLAttendance.FetchTodaysAttendance();
            return this.Returner(responseStatusInfo);
        }

        /// <summary>
        /// Action method to get attendances by employee id and month-year
        /// </summary>
        /// <param name="id">Employee Id</param>
        /// <param name="month">Attendance Month</param>
        /// <param name="year">Attendance Year</param>
        /// <returns>Instance of type IHttpActionResult</returns>
        [HttpGet]
        [Route("monthyear/{month}/{year}/employee/{id}")]
        public IHttpActionResult GetAttendanceByEmployeeIdAndMonthYear(int id, int month, int year)
        {
            ResponseStatusInfo responseStatusInfo = BLAttendance.FetchAttendanceByEmployeeIdAndMonthYear(id, month, year);
            return this.Returner(responseStatusInfo);
        }

        /// <summary>
        /// Action method to get all attendances by employee id an current month 
        /// </summary>
        /// <param name="id">Employee Id</param>
        /// <returns>Instance of type IHttpActionResult</returns>
        [HttpGet]
        [Route("employee/{id}/currentmonth")]
        public IHttpActionResult GetAttendanceByEmployeeIdForCurrentMonth(int id)
        {
            DateTime date = DateTime.Today;
            ResponseStatusInfo responseStatusInfo = BLAttendance.FetchAttendanceByEmployeeIdAndMonthYear(id, date.Month, date.Year);
            return this.Returner(responseStatusInfo);
        }


        //[HttpGet]
        //[Route("{id}")]
        //public IHttpActionResult GetAttendance(int id)
        //{
        //    ResponseStatusInfo responseStatusInfo = BLResource<ATD01>.FetchResource(id);
        //    return this.Returner(responseStatusInfo);
        //}

        /// <summary>
        /// Action method to post an attednace for an employee
        /// </summary>
        /// <param name="id">Employee Id</param>
        /// <param name="dayWorkHour">Day work hour</param>
        /// <returns>Instance of type IHttpActionResult</returns>
        [HttpPost]
        [Route("employee/{id}")]
        public IHttpActionResult PostAttendance(int id, JObject dayWorkHour)
        {
            ResponseStatusInfo responseStatusInfo = BLAttendance.AddAttendance(id, (double)dayWorkHour["dayWorkHour"]);
            return this.Returner(responseStatusInfo);
        }

        /// <summary>
        /// Action method to update an attendance
        /// </summary>
        /// <param name="id">Attendance Id</param>
        /// <param name="toUpdateJson">Attendance data to update in json format</param>
        /// <returns>Instance of type IHttpActionResult</returns>
        [HttpPatch]
        [Route("{id}")]
        public IHttpActionResult PatchAttendance(int id, JObject toUpdateJson)
        {
            ResponseStatusInfo responseStatusInfo = BLResource<ATD01>.UpdateResource(id, toUpdateJson);
            return this.Returner(responseStatusInfo);
        }

        /// <summary>
        /// Action method to delete an attendance
        /// </summary>
        /// <param name="id">Attendance Id</param>
        /// <returns>Instance of type IHttpActionResult</returns>
        [HttpDelete]
        [Route("{id}")]
        public IHttpActionResult DeleteAttendance(int id)
        {
            ResponseStatusInfo responseStatusInfo = BLResource<ATD01>.RemoveResource(id);
            return this.Returner(responseStatusInfo);
        }
    }
}
