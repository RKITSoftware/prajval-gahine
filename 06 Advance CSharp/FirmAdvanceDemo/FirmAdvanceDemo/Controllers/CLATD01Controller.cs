using FirmAdvanceDemo.Auth;
using FirmAdvanceDemo.BL;
using FirmAdvanceDemo.Models.DTO.DataAnnotations;
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
        #region Private Fields
        /// <summary>
        /// Instance of BLATD01Handler for handling attendance business logic.
        /// </summary>
        private readonly BLATD01Handler _objBLAttendance;
        #endregion

        #region Constructors
        /// <summary>
        /// Default constructor for CLATD01Controller.
        /// </summary>
        public CLATD01Controller()
        {
            _objBLAttendance = new BLATD01Handler();
        }
        #endregion

        #region Public Methods
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
        /// <param name="employeeID">Employee Id</param>
        /// <returns>Instance of type IHttpActionResult</returns>
        [HttpGet]
        [Route("employee/{employeeID}")]
        [AccessTokenAuthentication]
        [BasicAuthorization(Roles = "A,E")]
        public IHttpActionResult GetAttendanceByemployeeID(int employeeID)
        {
            Response response;
            response = GeneralUtility.ValidateAccess(employeeID);
            if (!response.IsError)
            {
                response = _objBLAttendance.RetrieveAttendanceByemployeeID(employeeID);
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
        [ValidateMonthYear]
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
        /// <param name="employeeID">Employee Id</param>
        /// <param name="year">Attendance Year</param>
        /// <param name="month">Attendance Month</param>
        /// <returns>HTTP response indicating the result of the operation.</returns>
        [HttpGet]
        [Route("employee/{employeeID}/{year}/{month}")]
        [AccessTokenAuthentication]
        [BasicAuthorization(Roles = "A,E")]
        [ValidateMonthYear]
        public IHttpActionResult GetAttendanceByemployeeIDAndMonthYear(int employeeID, int year, int month)
        {
            Response response;
            response = GeneralUtility.ValidateAccess(employeeID);
            if (!response.IsError)
            {
                response = _objBLAttendance.RetrieveAttendanceByemployeeIDAndMonthYear(employeeID, year, month);
            }
            return Ok(response);
        }

        /// <summary>
        /// Action method to retrieve all attendances by employee ID for the current month. Requires Admin or Employee role.
        /// </summary>
        /// <param name="employeeID">Employee Id</param>
        /// <returns>HTTP response indicating the result of the operation.</returns>
        [HttpGet]
        [Route("employee/{employeeID}/current-month")]
        [AccessTokenAuthentication]
        [BasicAuthorization(Roles = "A,E")]
        public IHttpActionResult GetAttendanceByemployeeIDForCurrentMonth(int employeeID)
        {
            DateTime date = DateTime.Today;

            Response response;
            response = GeneralUtility.ValidateAccess(employeeID);

            if (!response.IsError)
            {
                response = _objBLAttendance.RetrieveAttendanceByemployeeIDAndMonthYear(employeeID, date.Year, date.Month);
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
        #endregion
    }
}