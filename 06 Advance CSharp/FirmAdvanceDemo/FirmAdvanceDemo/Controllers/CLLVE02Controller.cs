using FirmAdvanceDemo.Auth;
using FirmAdvanceDemo.BL;
using FirmAdvanceDemo.Enums;
using FirmAdvanceDemo.Models.DTO;
using FirmAdvanceDemo.Utility;
using System;
using System.Web;
using System.Web.Http;

namespace FirmAdvanceDemo.Controllers
{
    /// <summary>
    /// Controller for managing leave operations.
    /// </summary>
    [RoutePrefix("api/leave")]
    public class CLLVE02Controller : ApiController
    {
        /// <summary>
        /// Instance of BLLVE02Handler.
        /// </summary>
        private readonly BLLVE02Handler _objBLLVE02Handler;

        /// <summary>
        /// Default constructor for CLLeaveController.
        /// </summary>
        public CLLVE02Controller()
        {
            _objBLLVE02Handler = new BLLVE02Handler();
        }

        /// <summary>
        /// Action method to retrieve all leave entries. Requires Admin role.
        /// </summary>
        /// <returns>HTTP response containing leave information.</returns>
        [HttpGet]
        [Route("")]
        [AccessTokenAuthentication]
        [BasicAuthorization(Roles = "A")]
        public IHttpActionResult GetLeave()
        {
            Response response = _objBLLVE02Handler.RetrieveLeave();
            return Ok(response);
        }

        /// <summary>
        /// Action method to retrieve a specific leave entry by ID. Requires Admin role.
        /// </summary>
        /// <param name="leaveId">The ID of the leave entry to retrieve.</param>
        /// <returns>HTTP response containing leave information.</returns>
        [HttpGet]
        [Route("{leaveId}")]
        [AccessTokenAuthentication]
        [BasicAuthorization(Roles = "A")]
        public IHttpActionResult GetLeave(int leaveId)
        {
            Response response = _objBLLVE02Handler.RetrieveLeave(leaveId);
            return Ok(response);
        }

        /// <summary>
        /// Action method to retrieve leave entries by status. Requires Admin role.
        /// </summary>
        /// <param name="leaveStatus">The status of leave entries to retrieve.</param>
        /// <returns>HTTP response containing leave information.</returns>
        [HttpGet]
        [Route("status/{leaveStatus}")]
        [AccessTokenAuthentication]
        [BasicAuthorization(Roles = "A")]
        public IHttpActionResult GetLeaveByStatus(EnmLeaveStatus leaveStatus)
        {
            Response response = _objBLLVE02Handler.RetrieveLeaveByStatus(leaveStatus);
            return Ok(response);
        }

        /// <summary>
        /// Action method to retrieve leave entries by employee ID. Requires Admin role.
        /// </summary>
        /// <param name="employeeID">The ID of the employee whose leave entries to retrieve.</param>
        /// <returns>HTTP response containing leave information.</returns>
        [HttpGet]
        [Route("employee/{employeeID}")]
        [AccessTokenAuthentication]
        [BasicAuthorization(Roles = "A")]
        public IHttpActionResult GetLeaveByEmployee(int employeeID)
        {
            Response response = _objBLLVE02Handler.RetrieveLeaveByEmployee(employeeID);
            return Ok(response);
        }

        /// <summary>
        /// Action method to retrieve leave entries by current employee's ID. Requires Employee role.
        /// </summary>
        /// <returns>HTTP response containing leave information.</returns>
        [HttpGet]
        [Route("employee")]
        [AccessTokenAuthentication]
        [BasicAuthorization(Roles = "E")]
        public IHttpActionResult GetLeaveByEmployee()
        {
            int employeeID = (int)HttpContext.Current.Items["employeeID"];
            Response response = _objBLLVE02Handler.RetrieveLeaveByEmployee(employeeID);
            return Ok(response);
        }

        /// <summary>
        /// Action method to retrieve leave entries by employee ID, year, and month. Requires Admin role.
        /// </summary>
        /// <param name="employeeID">The ID of the employee whose leave entries to retrieve.</param>
        /// <param name="year">The year for which leave entries are requested.</param>
        /// <param name="month">The month for which leave entries are requested.</param>
        /// <returns>HTTP response containing leave information.</returns>
        [HttpGet]
        [Route("employee/{employeeID}/monthly")]
        [AccessTokenAuthentication]
        [BasicAuthorization(Roles = "A")]
        public IHttpActionResult GetLeaveByEmployeeAndMonthYear(int employeeID, int year, int month)
        {
            Response response = _objBLLVE02Handler.RetrieveLeaveByEmployeeAndMonthYear(employeeID, year, month);
            return Ok(response);
        }

        /// <summary>
        /// Action method to retrieve leave entries by current employee's ID, year, and month. Requires Employee role.
        /// </summary>
        /// <param name="year">The year for which leave entries are requested.</param>
        /// <param name="month">The month for which leave entries are requested.</param>
        /// <returns>HTTP response containing leave information.</returns>
        [HttpGet]
        [Route("employee/monthly")]
        [AccessTokenAuthentication]
        [BasicAuthorization(Roles = "E")]
        public IHttpActionResult GetLeaveByEmployeeAndMonthYear(int year, int month)
        {
            int employeeID = (int)HttpContext.Current.Items["employeeID"];
            Response response = _objBLLVE02Handler.RetrieveLeaveByEmployeeAndMonthYear(employeeID, year, month);
            return Ok(response);
        }

        /// <summary>
        /// Action method to retrieve leave entries by employee ID and year. Requires Admin role.
        /// </summary>
        /// <param name="employeeID">The ID of the employee whose leave entries to retrieve.</param>
        /// <param name="year">The year for which leave entries are requested.</param>
        /// <returns>HTTP response containing leave information.</returns>
        [HttpGet]
        [Route("employee/{employeeID}/yearly")]
        [AccessTokenAuthentication]
        [BasicAuthorization(Roles = "A")]
        public IHttpActionResult GetLeaveByEmployeeAndYear(int employeeID, int year)
        {
            Response response = _objBLLVE02Handler.RetrieveLeaveByEmployeeAnYear(employeeID, year);
            return Ok(response);
        }

        /// <summary>
        /// Action method to retrieve leave entries by current employee's ID and year. Requires Employee role.
        /// </summary>
        /// <param name="year">The year for which leave entries are requested.</param>
        /// <returns>HTTP response containing leave information.</returns>
        [HttpGet]
        [Route("employee/yearly")]
        [AccessTokenAuthentication]
        [BasicAuthorization(Roles = "E")]
        public IHttpActionResult GetLeaveByEmployeeAndYear(int year)
        {
            int employeeID = (int)HttpContext.Current.Items["employeeID"];
            Response response = _objBLLVE02Handler.RetrieveLeaveByEmployeeAnYear(employeeID, year);
            return Ok(response);
        }

        /// <summary>
        /// Action method to retrieve leave entries by employee ID for the current year. Requires Admin role.
        /// </summary>
        /// <param name="employeeID">The ID of the employee whose leave entries to retrieve.</param>
        /// <returns>HTTP response containing leave information.</returns>
        [HttpGet]
        [Route("employee/{employeeID}/current-year")]
        [AccessTokenAuthentication]
        [BasicAuthorization(Roles = "A")]
        public IHttpActionResult GetLeaveByEmployeeForCurrentYear(int employeeID)
        {
            Response response = _objBLLVE02Handler.RetrieveLeaveByEmployeeAnYear(employeeID, DateTime.Now.Year);
            return Ok(response);
        }

        /// <summary>
        /// Action method to retrieve leave entries by current employee's ID for the current year. Requires Employee role.
        /// </summary>
        /// <returns>HTTP response containing leave information.</returns>
        [HttpGet]
        [Route("employee/current-year")]
        [AccessTokenAuthentication]
        [BasicAuthorization(Roles = "E")]
        public IHttpActionResult GetLeaveByEmployeeForCurrentYear()
        {
            int employeeID = (int)HttpContext.Current.Items["employeeID"];
            Response response = _objBLLVE02Handler.RetrieveLeaveByEmployeeAnYear(employeeID, DateTime.Now.Year);
            return Ok(response);
        }

        /// <summary>
        /// Action method to retrieve leave entries by employee ID for the current month. Requires Admin role.
        /// </summary>
        /// <param name="employeeID">The ID of the employee whose leave entries to retrieve.</param>
        /// <returns>HTTP response containing leave information.</returns>
        [HttpGet]
        [Route("employee/{employeeID}/current-month")]
        [AccessTokenAuthentication]
        [BasicAuthorization(Roles = "A")]
        public IHttpActionResult GetLeaveByEmployeeForCurrentMonth(int employeeID)
        {
            DateTime now = DateTime.Now;
            Response response = _objBLLVE02Handler.RetrieveLeaveByEmployeeAndMonthYear(employeeID, now.Year, now.Month);
            return Ok(response);
        }

        /// <summary>
        /// Action method to retrieve leave entries by current employee's ID for the current month. Requires Employee role.
        /// </summary>
        /// <returns>HTTP response containing leave information.</returns>
        [HttpGet]
        [Route("employee/current-month")]
        [AccessTokenAuthentication]
        [BasicAuthorization(Roles = "E")]
        public IHttpActionResult GetLeaveByEmployeeForCurrentMonth()
        {
            int employeeID = (int)HttpContext.Current.Items["employeeID"];
            DateTime now = DateTime.Now;
            Response response = _objBLLVE02Handler.RetrieveLeaveByEmployeeAndMonthYear(employeeID, now.Year, now.Month);
            return Ok(response);
        }

        /// <summary>
        /// Action method to retrieve leave entries by year and month. Requires Admin role.
        /// </summary>
        /// <param name="year">The year for which leave entries are requested.</param>
        /// <param name="month">The month for which leave entries are requested.</param>
        /// <returns>HTTP response containing leave information.</returns>
        [HttpGet]
        [Route("monthly")]
        [AccessTokenAuthentication]
        [BasicAuthorization(Roles = "A")]
        public IHttpActionResult GetLeaveByMonthYear(int year, int month)
        {
            Response response = _objBLLVE02Handler.RetrieveLeaveByMonthYear(year, month);
            return Ok(response);
        }

        /// <summary>
        /// Action method to retrieve leave entries by date. Requires Admin role.
        /// </summary>
        /// <param name="date">The date for which leave entries are requested.</param>
        /// <returns>HTTP response containing leave information.</returns>
        [HttpGet]
        [Route("date/{date}")]
        [AccessTokenAuthentication]
        [BasicAuthorization(Roles = "A")]
        public IHttpActionResult GetLeaveByDate(DateTime date)
        {
            Response response = _objBLLVE02Handler.RetrieveLeaveByDate(date);
            return Ok(response);
        }

        /// <summary>
        /// Action method to retrieve leave entries for today's date. Requires Admin role.
        /// </summary>
        /// <returns>HTTP response containing leave information.</returns>
        [HttpGet]
        [Route("today")]
        [AccessTokenAuthentication]
        [BasicAuthorization(Roles = "A")]
        public IHttpActionResult GetLeaveForToday()
        {
            Response response = _objBLLVE02Handler.RetrieveLeaveByDate(DateTime.Now.Date);
            return Ok(response);
        }

        /// <summary>
        /// Action method to create a new leave entry. Requires Employee role.
        /// </summary>
        /// <param name="objDTOLVE02">DTO object containing leave details.</param>
        /// <returns>HTTP response indicating the result of the operation.</returns>
        [HttpPost]
        [Route("")]
        [AccessTokenAuthentication]
        [BasicAuthorization(Roles = "E")]
        public IHttpActionResult PostLeave(DTOLVE02 objDTOLVE02)
        {
            Response response;
            _objBLLVE02Handler.Operation = EnmOperation.A;
            response = _objBLLVE02Handler.Prevalidate(objDTOLVE02);
            if (!response.IsError)
            {
                _objBLLVE02Handler.Presave(objDTOLVE02);
                response = _objBLLVE02Handler.Validate();
                if (!response.IsError)
                {
                    response = _objBLLVE02Handler.Save();
                }
            }
            return Ok(response);
        }

        /// <summary>
        /// Action method to update a leave entry. Requires Admin or Employee role.
        /// </summary>
        /// <param name="objDTOLVE02">DTO object containing leave details.</param>
        /// <returns>HTTP response indicating the result of the operation.</returns>
        [HttpPut]
        [Route("")]
        [AccessTokenAuthentication]
        [BasicAuthorization(Roles = "A,E")]
        public IHttpActionResult PutLeave(DTOLVE02 objDTOLVE02)
        {
            Response response;
            response = GeneralUtility.ValidateAccess(objDTOLVE02.E02F02);

            if (!response.IsError)
            {
                _objBLLVE02Handler.Operation = EnmOperation.E;
                response = _objBLLVE02Handler.Prevalidate(objDTOLVE02);
                if (!response.IsError)
                {
                    _objBLLVE02Handler.Presave(objDTOLVE02);
                    response = _objBLLVE02Handler.Validate();
                    if (!response.IsError)
                    {
                        response = _objBLLVE02Handler.Save();
                    }
                }
            }
            return Ok(response);
        }

        /// <summary>
        /// Action method to delete a leave entry. Requires Admin or Employee role.
        /// </summary>
        /// <param name="leaveId">The ID of the leave to delete.</param>
        /// <returns>HTTP response indicating the result of the operation.</returns>
        [HttpDelete]
        [Route("{leaveId}")]
        [AccessTokenAuthentication]
        [BasicAuthorization(Roles = "A,E")]
        public IHttpActionResult DeleteLeave(int leaveId)
        {
            Response response = _objBLLVE02Handler.ValidateDelete(leaveId);
            if (!response.IsError)
            {
                response = _objBLLVE02Handler.Delete(leaveId);
            }
            return Ok(response);
        }
    }
}
