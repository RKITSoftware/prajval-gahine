using FirmAdvanceDemo.Auth;
using FirmAdvanceDemo.BL;
using FirmAdvanceDemo.Utitlity;
using Newtonsoft.Json.Linq;
using System;
using System.Linq;
using System.Security.Claims;
using System.Web.Http;

namespace FirmAdvanceDemo.Controllers
{
    [RoutePrefix("api/attendance")]
    public class CLAttendanceController : BaseController
    {
        /// <summary>
        /// Instance of BLAttendance
        /// </summary>
        private readonly BLAttendance _objBLAttendance;

        /// <summary>
        /// Default constructor for CLAttendanceController
        /// </summary>
        public CLAttendanceController()
        {
            _objBLAttendance = new BLAttendance();
        }

        /// <summary>
        /// Action method to get all attendances of employees'
        /// </summary>
        /// <returns>Instance of type IHttpActionResult</returns>
        [HttpGet]
        [Route("")]
        [BasicAuthorization(Roles = "admin")]
        public IHttpActionResult GetAttendances()
        {
            ResponseStatusInfo responseStatusInfo = _objBLAttendance.FetchResource();
            return Returner(responseStatusInfo);
        }

        /// <summary>
        /// Action method to get an employee's attendances
        /// </summary>
        /// <param name="EmployeeId">Employee Id</param>
        /// <returns>Instance of type IHttpActionResult</returns>
        [HttpGet]
        [Route("employee/{id}")]
        [BasicAuthorization(Roles = "admin")]
        public IHttpActionResult GetAttendanceByEmployeeId(int EmployeeId)
        {
            ResponseStatusInfo responseStatusInfo = _objBLAttendance.FetchAttendanceByEmployeeId(EmployeeId);
            return Returner(responseStatusInfo);
        }

        /// <summary>
        /// Action method to get all employees attendances by month-year
        /// </summary>
        /// <param name="month">Attendance Month</param>
        /// <param name="year">Attendance Year</param>
        /// <returns>Instance of type IHttpActionResult</returns>
        [HttpGet]
        [Route("monthyear/{month}/{year}")]
        [BasicAuthorization(Roles = "admin")]
        public IHttpActionResult GetAttendanceByMonthYear(int month, int year)
        {
            ResponseStatusInfo responseStatusInfo = _objBLAttendance.FetchAttendanceByMonthYear(month, year);
            return Returner(responseStatusInfo);
        }

        /// <summary>
        /// Action method to get today's attendances of all employees
        /// </summary>
        /// <returns>Instance of type IHttpActionResult</returns>
        [HttpGet]
        [Route("today")]
        [BasicAuthorization(Roles = "admin")]
        public IHttpActionResult GetTodaysAttendance()
        {
            ResponseStatusInfo responseStatusInfo = _objBLAttendance.FetchTodaysAttendance();
            return Returner(responseStatusInfo);
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
        [BasicAuthorization(Roles = "admin")]
        public IHttpActionResult GetAttendanceByEmployeeIdAndMonthYear(int id, int month, int year)
        {
            ResponseStatusInfo responseStatusInfo = _objBLAttendance.FetchAttendanceByEmployeeIdAndMonthYear(id, month, year);
            return Returner(responseStatusInfo);
        }

        /// <summary>
        /// Action method to get all attendances by employee id an current month 
        /// </summary>
        /// <param name="id">Employee Id</param>
        /// <returns>Instance of type IHttpActionResult</returns>
        [HttpGet]
        [Route("employee/{id}/currentmonth")]
        [BasicAuthorization(Roles = "admin")]
        public IHttpActionResult GetAttendanceByEmployeeIdForCurrentMonth(int id)
        {
            DateTime date = DateTime.Today;
            ResponseStatusInfo responseStatusInfo = _objBLAttendance.FetchAttendanceByEmployeeIdAndMonthYear(id, date.Month, date.Year);
            return Returner(responseStatusInfo);
        }


        //[HttpGet]
        //[Route("{id}")]
        //public IHttpActionResult GetAttendance(int id)
        //{
        //    ResponseStatusInfo responseStatusInfo = _objBLAttendance.FetchResource(id);
        //    return Returner(responseStatusInfo);
        //}

        /// <summary>
        /// Action method to post an attednace for an employee
        /// </summary>
        /// <param name="id">Employee Id</param>
        /// <param name="dayWorkHour">Day work hour</param>
        /// <returns>Instance of type IHttpActionResult</returns>
        [HttpPost]
        [Route("employee/{id}")]
        [BasicAuthorization(Roles = "employee")]
        [Obsolete]
        public IHttpActionResult PostAttendance(int id, JObject dayWorkHour)
        {
            ClaimsIdentity identity = (ClaimsIdentity)User.Identity;
            int EmployeeId = int.Parse(
                identity.Claims.Where(c => c.Type == "EmployeeId")
                .Select(c => c.Value)
                .SingleOrDefault()
            );

            ResponseStatusInfo responseStatusInfo = _objBLAttendance.AddAttendance(id, (double)dayWorkHour["dayWorkHour"]);
            return Returner(responseStatusInfo);
        }

        /// <summary>
        /// Action method to update an attendance
        /// </summary>
        /// <param name="id">Attendance Id</param>
        /// <param name="toUpdateJson">Attendance data to update in json format</param>
        /// <returns>Instance of type IHttpActionResult</returns>
        [HttpPatch]
        [Route("{id}")]
        [BasicAuthorization(Roles = "admin")]
        public IHttpActionResult PatchAttendance(int id, JObject toUpdateJson)
        {
            ResponseStatusInfo responseStatusInfo = _objBLAttendance.UpdateResource(id, toUpdateJson);
            return Returner(responseStatusInfo);
        }

        /// <summary>
        /// Action method to delete an attendance
        /// </summary>
        /// <param name="id">Attendance Id</param>
        /// <returns>Instance of type IHttpActionResult</returns>
        [HttpDelete]
        [BasicAuthorization(Roles = "admin")]
        [Route("{id}")]
        public IHttpActionResult DeleteAttendance(int id)
        {
            ResponseStatusInfo responseStatusInfo = _objBLAttendance.RemoveResource(id);
            return Returner(responseStatusInfo);
        }
    }
}
