using FirmAdvanceDemo.BL;
using FirmAdvanceDemo.Enums;
using FirmAdvanceDemo.Models.DTO;
using FirmAdvanceDemo.Utitlity;
using System;
using System.Web.Http;

namespace FirmAdvanceDemo.Controllers
{
    [RoutePrefix("api/attendance")]
    public class CLATD01Controller : ApiController
    {
        /// <summary>
        /// Instance of BLAttendance
        /// </summary>
        private readonly BLATD01Handler _objBLAttendance;

        /// <summary>
        /// Default constructor for CLAttendanceController
        /// </summary>
        public CLATD01Controller()
        {
            _objBLAttendance = new BLATD01Handler();
        }

        /// <summary>
        /// Action method to get all attendances of employees'
        /// </summary>
        /// <returns>Instance of type IHttpActionResult</returns>
        [HttpGet]
        [Route("")]
        //[BasicAuthorization(Roles = "admin")]
        public IHttpActionResult GetAttendance()
        {
            Response response = _objBLAttendance.RetrieveAttendance();
            return Ok(response);
        }

        /// <summary>
        /// Action method to get an employee's attendances
        /// </summary>
        /// <param name="id">Employee Id</param>
        /// <returns>Instance of type IHttpActionResult</returns>
        [HttpGet]
        [Route("{id}")]
        //[BasicAuthorization(Roles = "admin")]
        public IHttpActionResult GetAttendance(int id)
        {
            Response response = _objBLAttendance.RetrieveAttendance(id);
            return Ok(response);
        }

        [HttpGet]
        [Route("employee/{id}")]
        public IHttpActionResult GetAttendanceByEmployeeId(int employeeId)
        {
            Response response = _objBLAttendance.RetrieveAttendanceByEmployeeId(employeeId);
            return Ok(response);
        }

        /// <summary>
        /// Action method to get all employees attendances by month-year
        /// </summary>
        /// <param name="month">Attendance Month</param>
        /// <param name="year">Attendance Year</param>
        /// <returns>Instance of type IHttpActionResult</returns>
        [HttpGet]
        [Route("")]
        //[BasicAuthorization(Roles = "admin")]
        public IHttpActionResult GetAttendanceByMonthYear(int year, int month)
        {
            Response reponse = _objBLAttendance.RetrieveAttendanceByMonthYear(year, month);
            return Ok(reponse);
        }

        /// <summary>
        /// Action method to get today's attendances of all employees
        /// </summary>
        /// <returns>Instance of type IHttpActionResult</returns>
        [HttpGet]
        [Route("today")]
        //[BasicAuthorization(Roles = "admin")]
        public IHttpActionResult GetAttendanceForToday()
        {
            Response response = _objBLAttendance.RetrieveAttendanceForToday();
            return Ok(response);
        }

        /// <summary>
        /// Action method to get attendances by employee ID and month-year
        /// </summary>
        /// <param name="employeeId">Employee Id</param>
        /// <param name="month">Attendance Month</param>
        /// <param name="year">Attendance Year</param>
        /// <returns>Instance of type IHttpActionResult</returns>
        [HttpGet]
        [Route("employee/{employeeId}")]
        //[BasicAuthorization(Roles = "admin")]
        public IHttpActionResult GetAttendanceByEmployeeIdAndMonthYear(int employeeId, int year, int month)
        {
            Response response = _objBLAttendance.RetrieveAttendanceByEmployeeIdAndMonthYear(employeeId, year, month);
            return Ok(response);
        }

        /// <summary>
        /// Action method to get all attendances by employee ID an current month 
        /// </summary>
        /// <param name="employeeId">Employee Id</param>
        /// <returns>Instance of type IHttpActionResult</returns>
        [HttpGet]
        [Route("employee/{id}/current-month")]
        //[BasicAuthorization(Roles = "admin")]
        public IHttpActionResult GetAttendanceByEmployeeIdForCurrentMonth(int employeeId)
        {
            DateTime date = DateTime.Today;
            Response response = _objBLAttendance.RetrieveAttendanceByEmployeeIdAndMonthYear(employeeId, date.Month, date.Year);
            return Ok(response);
        }

        /// <summary>
        /// Action method to post an attednace for an employee
        /// </summary>
        /// <param name="id">Employee Id</param>
        /// <param name="dayWorkHour">Day work hour</param>
        /// <returns>Instance of type IHttpActionResult</returns>
        [HttpPost]
        [Route("")]
        //[BasicAuthorization(Roles = "employee")]
        [Obsolete]
        public IHttpActionResult PostAttendance(DTOATD01 objDTOATD01)
        {
            Response response;
            _objBLAttendance.Operation = EnmOperation.A;
            response = _objBLAttendance.Prevalidate(objDTOATD01);
            if (!response.IsError)
            {
                _objBLAttendance.Presave(objDTOATD01);
                response = _objBLAttendance.Validate();
                if (!response.IsError)
                {
                    _objBLAttendance.Save();
                }
            }
            return Ok(response);
        }

        /// <summary>
        /// Action method to update an attendance
        /// </summary>
        /// <param name="id">Attendance Id</param>
        /// <param name="toUpdateJson">Attendance data to update in json format</param>
        /// <returns>Instance of type IHttpActionResult</returns>
        [HttpPut]
        [Route("{id}")]
        //[BasicAuthorization(Roles = "admin")]
        public IHttpActionResult PatchAttendance(DTOATD01 objDTOATD01)
        {
            Response response;
            _objBLAttendance.Operation = EnmOperation.E;
            response = _objBLAttendance.Prevalidate(objDTOATD01);
            if (!response.IsError)
            {
                _objBLAttendance.Presave(objDTOATD01);
                response = _objBLAttendance.Validate();
                if (!response.IsError)
                {
                    _objBLAttendance.Save();
                }
            }
            return Ok(response);
        }

        /// <summary>
        /// Action method to delete an attendance
        /// </summary>
        /// <param name="id">Attendance Id</param>
        /// <returns>Instance of type IHttpActionResult</returns>
        [HttpDelete]
        //[BasicAuthorization(Roles = "admin")]
        [Route("{id}")]
        public IHttpActionResult DeleteAttendance(int id)
        {
            Response response = _objBLAttendance.RemoveResource(id);
            return Ok(response);
        }


        [HttpPost]
        [Route("api/attendance/evaluate-eod-punches")]
        public IHttpActionResult EvaluateEndOfDayPunches(DateTime date)
        {
            // check378 - check if model state is invalid when we donot pass date in query string
            if (!ModelState.IsValid)
            {
                date = DateTime.Now;
            }

            Response response = _objBLAttendance.ProcessEndOfDayPunches(date);

            return Ok(response);
        }
    }
}
