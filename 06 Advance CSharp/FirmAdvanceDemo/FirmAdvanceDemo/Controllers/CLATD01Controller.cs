using FirmAdvanceDemo.Auth;
using FirmAdvanceDemo.BL;
using FirmAdvanceDemo.Utility;
using System;
using System.Web.Http;

namespace FirmAdvanceDemo.Controllers
{
    /// <summary>
    /// Controller for managing attendance-related operations.
    /// </summary>
    [RoutePrefix("api/attendance")]
    public class CLATD01Controller : ApiController
    {
        /// <summary>
        /// Instance of BLATD01Handler for handling attendance business logic.
        /// </summary>
        private readonly BLATD01Handler _objBLAttendance;

        /// <summary>
        /// Default constructor for CLATD01Controller.
        /// </summary>

        public CLATD01Controller()
        {
            _objBLAttendance = new BLATD01Handler();
        }

        /// <summary>
        /// Action method to retrieve all attendances of employees. Requires Admin role.
        /// </summary>
        /// <returns>HTTP response indicating the result of the operation.</returns>
        [HttpGet]
        [Route("")]
        [AccessTokenAuthentication]
        [BasicAuthorization(Roles = "A")]
        public IHttpActionResult GetAttendance()
        {
            Response response = _objBLAttendance.RetrieveAttendance();
            return Ok(response);
        }

        /// <summary>
        /// Action method to retrieve an employee's attendance. Requires Admin role.
        /// </summary>
        /// <param name="attendanceID">The ID of the attendance to retrieve.</param>
        /// <returns>HTTP response indicating the result of the operation.</returns>
        [HttpGet]
        [Route("{attendanceID}")]
        [AccessTokenAuthentication]
        [BasicAuthorization(Roles = "A")]
        public IHttpActionResult GetAttendance(int attendanceID)
        {
            Response response = _objBLAttendance.RetrieveAttendance(attendanceID);
            return Ok(response);
        }

        /// <summary>
        /// Action method to retrieve an employee's attendance by employee ID. Requires Admin or Employee role.
        /// </summary>
        /// <param name="employeeId">Employee Id</param>
        /// <returns>Instance of type IHttpActionResult</returns>
        [HttpGet]
        [Route("employee/{employeeId}")]
        [AccessTokenAuthentication]
        [BasicAuthorization(Roles = "A,E")]
        public IHttpActionResult GetAttendanceByEmployeeId(int employeeId)
        {
            Response response;
            response = GeneralUtility.ValidateAccess(employeeId);
            if (!response.IsError)
            {
                response = _objBLAttendance.RetrieveAttendanceByEmployeeId(employeeId);
            }
            return Ok(response);
        }

        /// <summary>
        /// Action method to retrieve attendances by month and year. Requires Admin role.
        /// </summary>
        /// <param name="year">Attendance Year</param>
        /// <param name="month">Attendance Month</param>
        /// <returns>HTTP response indicating the result of the operation.</returns>
        [HttpGet]
        [Route("monthly")]
        [AccessTokenAuthentication]
        [BasicAuthorization(Roles = "A")]
        public IHttpActionResult GetAttendanceByMonthYear(int year, int month)
        {
            Response reponse = _objBLAttendance.RetrieveAttendanceByMonthYear(year, month);
            return Ok(reponse);
        }

        /// <summary>
        /// Action method to retrieve today's attendances of all employees. Requires Admin role.
        /// </summary>
        /// <returns>HTTP response indicating the result of the operation.</returns>
        [HttpGet]
        [Route("today")]
        [AccessTokenAuthentication]
        [BasicAuthorization(Roles = "A")]
        public IHttpActionResult GetAttendanceForToday()
        {
            Response response = _objBLAttendance.RetrieveAttendanceForToday();
            return Ok(response);
        }

        /// <summary>
        /// Action method to retrieve attendances by employee ID and month-year. Requires Admin or Employee role.
        /// </summary>
        /// <param name="employeeId">Employee Id</param>
        /// <param name="year">Attendance Year</param>
        /// <param name="month">Attendance Month</param>
        /// <returns>HTTP response indicating the result of the operation.</returns>
        [HttpGet]
        [Route("employee/{employeeId}/{year}/{month}")]
        [AccessTokenAuthentication]
        [BasicAuthorization(Roles = "A,E")]
        public IHttpActionResult GetAttendanceByEmployeeIdAndMonthYear(int employeeId, int year, int month)
        {
            Response response;
            response = GeneralUtility.ValidateAccess(employeeId);
            if (!response.IsError)
            {
                response = _objBLAttendance.RetrieveAttendanceByEmployeeIdAndMonthYear(employeeId, year, month);
            }
            return Ok(response);
        }

        /// <summary>
        /// Action method to retrieve all attendances by employee ID for the current month. Requires Admin or Employee role.
        /// </summary>
        /// <param name="employeeId">Employee Id</param>
        /// <returns>HTTP response indicating the result of the operation.</returns>
        [HttpGet]
        [Route("employee/{employeeId}/current-month")]
        [AccessTokenAuthentication]
        [BasicAuthorization(Roles = "A,E")]
        public IHttpActionResult GetAttendanceByEmployeeIdForCurrentMonth(int employeeId)
        {
            DateTime date = DateTime.Today;

            Response response;
            response = GeneralUtility.ValidateAccess(employeeId);

            if (!response.IsError)
            {
                response = _objBLAttendance.RetrieveAttendanceByEmployeeIdAndMonthYear(employeeId, date.Year, date.Month);
            }
            return Ok(response);
        }

        /// <summary>
        /// Action method to delete an attendance. Requires Admin role.
        /// </summary>
        /// <param name="attendanceID">Attendance Id</param>
        /// <returns>HTTP response indicating the result of the operation.</returns>
        [HttpDelete]
        [Route("{attendanceID}")]
        [AccessTokenAuthentication]
        [BasicAuthorization(Roles = "A")]
        public IHttpActionResult DeleteAttendance(int attendanceID)
        {
            // Delete an attendance
            Response response = _objBLAttendance.ValidateDelete(attendanceID);
            if (!response.IsError)
            {
                response = _objBLAttendance.Delete(attendanceID);
            }
            return Ok(response);
        }

        /// <summary>
        /// Action method to evaluate end-of-day punches. Requires Admin role.
        /// </summary>
        /// <param name="date">Date for which to evaluate end-of-day punches.</param>
        /// <returns>HTTP response indicating the result of the operation.</returns>
        [HttpPost]
        [Route("evaluate-eod-punches/today")]
        [AccessTokenAuthentication]
        [BasicAuthorization(Roles = "A")]
        public IHttpActionResult ProcessEoDPunchesForToday()
        {
            Response response;
            _objBLAttendance.PresaveEoDPunchesForDate(DateTime.Now);
            response = _objBLAttendance.ValidateEoDAttendance();
            if (!response.IsError)
            {
                response = _objBLAttendance.SaveAttendance();
            }
            return Ok(response);
        }

        /// <summary>
        /// Action method to evaluate end-of-day punches. Requires Admin role.
        /// </summary>
        /// <param name="date">Date for which to evaluate end-of-day punches.</param>
        /// <returns>HTTP response indicating the result of the operation.</returns>
        [HttpPost]
        [Route("evaluate-eod-punches")]
        [AccessTokenAuthentication]
        [BasicAuthorization(Roles = "A")]
        public IHttpActionResult ProcessEoDPunchesForDate(DateTime date)
        {
            Response response;
            _objBLAttendance.PresaveEoDPunchesForDate(date);
            response = _objBLAttendance.ValidateEoDAttendance();
            if (!response.IsError)
            {
                response = _objBLAttendance.SaveAttendance();
            }
            return Ok(response);
        }
    }
}