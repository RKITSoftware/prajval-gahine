using FirmAdvanceDemo.Auth;
using FirmAdvanceDemo.BL;
using FirmAdvanceDemo.Enums;
using FirmAdvanceDemo.Models.DTO;
using FirmAdvanceDemo.Models.DTO.DataAnnotations;
using FirmAdvanceDemo.Models.DTO.FIlters;
using FirmAdvanceDemo.Utility;
using System;
using System.Web.Http;

namespace FirmAdvanceDemo.Controllers
{
    /// <summary>
    /// Controller for managing leave operations.
    /// </summary>
    [RoutePrefix("api/leave")]
    public class CLLVE02Controller : ApiController
    {
        #region Private Fields
        /// <summary>
        /// Instance of BLLVE02Handler.
        /// </summary>
        private readonly BLLVE02Handler _objBLLVE02Handler;
        #endregion

        #region Constructors
        /// <summary>
        /// Default constructor for CLLeaveController.
        /// </summary>
        public CLLVE02Controller()
        {
            _objBLLVE02Handler = new BLLVE02Handler();
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Action method to retrieve all leave entries.
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
        /// Action method to retrieve a specific leave entry by ID.
        /// </summary>
        /// <param name="leaveID">The ID of the leave entry to retrieve.</param>
        /// <returns>HTTP response containing leave information.</returns>
        [HttpGet]
        [Route("{leaveId}")]
        [AccessTokenAuthentication]
        [BasicAuthorization(Roles = "A")]
        public IHttpActionResult GetLeave(int leaveID)
        {
            Response response = _objBLLVE02Handler.RetrieveLeave(leaveID);
            return Ok(response);
        }

        /// <summary>
        /// Action method to retrieve leave entries by employee ID.
        /// </summary>
        /// <param name="employeeID">The ID of the employee whose leave entries to retrieve.</param>
        /// <returns>HTTP response containing leave information.</returns>
        [HttpGet]
        [Route("employee/{employeeID}")]
        [AccessTokenAuthentication]
        [BasicAuthorization(Roles = "A,E")]
        public IHttpActionResult GetLeaveByEmployee(int employeeID)
        {
            Response response = GeneralUtility.ValidateAccess(employeeID);
            if (!response.IsError)
            {
                response = _objBLLVE02Handler.RetrieveLeaveByEmployee(employeeID);
            }
            return Ok(response);
        }

        /// <summary>
        /// Action method to retrieve leave entries by employee ID, year, and month.
        /// </summary>
        /// <param name="employeeID">The ID of the employee whose leave entries to retrieve.</param>
        /// <param name="year">The year for which leave entries are requested.</param>
        /// <param name="month">The month for which leave entries are requested.</param>
        /// <returns>HTTP response containing leave information.</returns>
        [HttpGet]
        [Route("employee/{employeeID}/monthly")]
        [AccessTokenAuthentication]
        [BasicAuthorization(Roles = "A,E")]
        [ValidateMonthYear]
        public IHttpActionResult GetLeaveByEmployeeAndMonthYear(int employeeID, int year, int month)
        {
            Response response = GeneralUtility.ValidateAccess(employeeID);
            if (!response.IsError)
            {
                response = _objBLLVE02Handler.RetrieveLeaveByEmployeeAndMonthYear(employeeID, year, month);
            }
            return Ok(response);
        }

        /// <summary>
        /// Action method to retrieve leave entries by employee ID and year.
        /// </summary>
        /// <param name="employeeID">The ID of the employee whose leave entries to retrieve.</param>
        /// <param name="year">The year for which leave entries are requested.</param>
        /// <returns>HTTP response containing leave information.</returns>
        [HttpGet]
        [Route("employee/{employeeID}/yearly")]
        [AccessTokenAuthentication]
        [BasicAuthorization(Roles = "A,E")]
        [ValidateMonthYear]
        public IHttpActionResult GetLeaveByEmployeeAndYear(int employeeID, int year)
        {
            Response response = GeneralUtility.ValidateAccess(employeeID);
            if (!response.IsError)
            {
                response = _objBLLVE02Handler.RetrieveLeaveByEmployeeAndYear(employeeID, year);
            }
            return Ok(response);
        }

        /// <summary>
        /// Action method to retrieve leave entries by employee ID for the current year.
        /// </summary>
        /// <param name="employeeID">The ID of the employee whose leave entries to retrieve.</param>
        /// <returns>HTTP response containing leave information.</returns>
        [HttpGet]
        [Route("employee/{employeeID}/current-year")]
        [AccessTokenAuthentication]
        [BasicAuthorization(Roles = "A,E")]
        public IHttpActionResult GetLeaveByEmployeeForCurrentYear(int employeeID)
        {
            Response response = GeneralUtility.ValidateAccess(employeeID);
            if (!response.IsError)
            {
                response = _objBLLVE02Handler.RetrieveLeaveByEmployeeAndYear(employeeID, DateTime.Now.Year);
            }
            return Ok(response);
        }

        /// <summary>
        /// Action method to retrieve leave entries by current employee's ID for the current month.
        /// </summary>
        /// <returns>HTTP response containing leave information.</returns>
        [HttpGet]
        [Route("employee/{employeeID}current-month")]
        [AccessTokenAuthentication]
        [BasicAuthorization(Roles = "A,E")]
        public IHttpActionResult GetLeaveByEmployeeForCurrentMonth(int employeeID)
        {
            Response response;
            response = GeneralUtility.ValidateAccess(employeeID);
            if (!response.IsError)
            {
                DateTime now = DateTime.Now;
                response = _objBLLVE02Handler.RetrieveLeaveByEmployeeAndMonthYear(employeeID, now.Year, now.Month);
            }

            return Ok(response);
        }

        /// <summary>
        /// Action method to retrieve leave entries by year and month.
        /// </summary>
        /// <param name="year">The year for which leave entries are requested.</param>
        /// <param name="month">The month for which leave entries are requested.</param>
        /// <returns>HTTP response containing leave information.</returns>
        [HttpGet]
        [Route("monthly")]
        [AccessTokenAuthentication]
        [BasicAuthorization(Roles = "A")]
        [ValidateMonthYear]
        public IHttpActionResult GetLeaveByMonthYear(int year, int month)
        {
            Response response = _objBLLVE02Handler.RetrieveLeaveByMonthYear(year, month);
            return Ok(response);
        }

        /// <summary>
        /// Action method to retrieve leave entries by date.
        /// </summary>
        /// <param name="date">The date for which leave entries are requested.</param>
        /// <returns>HTTP response containing leave information.</returns>
        [HttpGet]
        [Route("date/{date}")]
        [AccessTokenAuthentication]
        [BasicAuthorization(Roles = "A")]
        public IHttpActionResult GetLeaveForDate(DateTime date)
        {
            Response response = _objBLLVE02Handler.RetrieveLeaveForDate(date);
            return Ok(response);
        }

        /// <summary>
        /// Action method to retrieve leave entries for today's date.
        /// </summary>
        /// <returns>HTTP response containing leave information.</returns>
        [HttpGet]
        [Route("today")]
        [AccessTokenAuthentication]
        [BasicAuthorization(Roles = "A")]
        public IHttpActionResult GetLeaveForToday()
        {
            Response response = _objBLLVE02Handler.RetrieveLeaveForDate(DateTime.Now.Date);
            return Ok(response);
        }

        /// <summary>
        /// Retrieves leave entries by status.
        /// </summary>
        /// <param name="leaveStatus">The status of the leave entries to retrieve.</param>
        /// <returns>An IHttpActionResult containing the retrieved leave entries.</returns>
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
        /// Action method to create a new leave entry.
        /// </summary>
        /// <param name="objDTOLVE02">DTO object containing leave details.</param>
        /// <returns>HTTP response indicating the result of the operation.</returns>
        [HttpPost]
        [Route("")]
        [AccessTokenAuthentication]
        [BasicAuthorization(Roles = "E")]
        [ValidateModel]
        public IHttpActionResult PostLeave(DTOLVE02 objDTOLVE02)
        {
            Response response = GeneralUtility.ValidateAccess(objDTOLVE02.E02F02);

            if (!response.IsError)
            {
                _objBLLVE02Handler.Operation = EnmOperation.A;
                response = _objBLLVE02Handler.Prevalidate(objDTOLVE02);
                if (!response.IsError)
                {
                    _objBLLVE02Handler.Presave(objDTOLVE02);

                    if (!response.IsError)
                    {
                        response = _objBLLVE02Handler.Save();
                    }
                }
            }
            return Ok(response);
        }

        /// <summary>
        /// Action method to update a leave entry.
        /// </summary>
        /// <param name="objDTOLVE02">DTO object containing leave details.</param>
        /// <returns>HTTP response indicating the result of the operation.</returns>
        [HttpPut]
        [Route("")]
        [AccessTokenAuthentication]
        [BasicAuthorization(Roles = "E")]
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
        /// Updates the status of a leave entry.
        /// </summary>
        /// <param name="leaveID">The ID of the leave entry to update.</param>
        /// <param name="toLeaveStatus">The new status to update to.</param>
        /// <returns>An IHttpActionResult indicating the result of the operation.</returns>
        [HttpPatch]
        [Route("status/{toLeaveStatus}")]
        [AccessTokenAuthentication]
        [BasicAuthorization(Roles = "A")]
        public IHttpActionResult PatchLeaveStatus(int leaveID, EnmLeaveStatus toLeaveStatus)
        {
            Response response;

            response = _objBLLVE02Handler.ValidateUpdateLeaveStatus(leaveID, toLeaveStatus);

            if (!response.IsError)
            {
                response = _objBLLVE02Handler.SaveLeaveStatus(leaveID, toLeaveStatus);
            }
            return Ok(response);
        }

        /// <summary>
        /// Action method to delete a leave entry.
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
        #endregion
    }
}
