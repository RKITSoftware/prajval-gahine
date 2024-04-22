using FirmAdvanceDemo.Auth;
using FirmAdvanceDemo.BL;
using FirmAdvanceDemo.Enums;
using FirmAdvanceDemo.Models.DTO;
using FirmAdvanceDemo.Utitlity;
using System;
using System.Web;
using System.Web.Http;
using System.Web.Security;

namespace FirmAdvanceDemo.Controllers
{
    [RoutePrefix("api/leave")]
    public class CLLVE02Controller : ApiController
    {
        /// <summary>
        /// Instance of BLLeave
        /// </summary>
        private readonly BLLVE02Handler _objBLLVE02Handler;

        /// <summary>
        /// Default constructor for CLLeaveController
        /// </summary>
        public CLLVE02Controller()
        {
            _objBLLVE02Handler = new BLLVE02Handler();
        }

        [HttpGet]
        [Route("")]
        [AccessTokenAuthentication]
        [BasicAuthorization(Roles = "A")]
        public IHttpActionResult GetLeave()
        {
            Response response = _objBLLVE02Handler.RetrieveLeave();
            return Ok(response);
        }

        [HttpGet]
        [Route("{leaveId}")]
        [AccessTokenAuthentication]
        [BasicAuthorization(Roles = "A")]
        public IHttpActionResult GetLeave(int leaveId)
        {
            Response response = _objBLLVE02Handler.RetrieveLeave(leaveId);
            return Ok(response);
        }

        [HttpGet]
        [Route("status/{leaveStatus}")]
        [AccessTokenAuthentication]
        [BasicAuthorization(Roles = "A")]
        public IHttpActionResult GetLeaveByStatus(EnmLeaveStatus leaveStatus)
        {
            Response response = _objBLLVE02Handler.RetrieveLeaveByStatus(leaveStatus);
            return Ok(response);
        }

        [HttpGet]
        [Route("employee/{employeeID}")]
        [AccessTokenAuthentication]
        [BasicAuthorization(Roles = "A")]
        public IHttpActionResult GetLeaveByEmployee(int employeeID)
        {
            Response response = _objBLLVE02Handler.RetrieveLeaveByEmployee(employeeID);
            return Ok(response);
        }

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

        [HttpGet]
        [Route("employee/{employeeID}/monthly")]
        [AccessTokenAuthentication]
        [BasicAuthorization(Roles = "A")]
        public IHttpActionResult GetLeaveByEmployeeAndMonthYear(int employeeID, int year, int month)
        {
            Response response = _objBLLVE02Handler.RetrieveLeaveByEmployeeAndMonthYear(employeeID, year, month);
            return Ok(response);
        }

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

        [HttpGet]
        [Route("employee/{employeeID}/yearly")]
        [AccessTokenAuthentication]
        [BasicAuthorization(Roles = "A")]
        public IHttpActionResult GetLeaveByEmployeeAndYear(int employeeID, int year)
        {
            Response response = _objBLLVE02Handler.RetrieveLeaveByEmployeeAnYear(employeeID, year);
            return Ok(response);
        }

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

        [HttpGet]
        [Route("employee/{employeeID}/current-year")]
        [AccessTokenAuthentication]
        [BasicAuthorization(Roles = "A")]
        public IHttpActionResult GetLeaveByEmployeeForCurrentYear(int employeeID)
        {
            Response response = _objBLLVE02Handler.RetrieveLeaveByEmployeeAnYear(employeeID, DateTime.Now.Year);
            return Ok(response);
        }

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

        [HttpGet]
        [Route("monthly")]
        [AccessTokenAuthentication]
        [BasicAuthorization(Roles = "A")]
        public IHttpActionResult GetLeaveByMonthYear(int year, int month)
        {
            Response response = _objBLLVE02Handler.RetrieveLeaveByMonthYear(year, month);
            return Ok(response);
        }

        [HttpGet]
        [Route("date/{date}")]
        [AccessTokenAuthentication]
        [BasicAuthorization(Roles = "A")]
        public IHttpActionResult GetLeaveByDate(DateTime date)
        {
            Response response = _objBLLVE02Handler.RetrieveLeaveByDate(date);
            return Ok(response);
        }

        [HttpGet]
        [Route("today")]
        [AccessTokenAuthentication]
        [BasicAuthorization(Roles = "A")]
        public IHttpActionResult GetLeaveForToday()
        {
            Response response = _objBLLVE02Handler.RetrieveLeaveByDate(DateTime.Now.Date);
            return Ok(response);
        }

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

        [HttpPut]
        [Route("")]
        [AccessTokenAuthentication]
        [BasicAuthorization(Roles = "A,E")]
        public IHttpActionResult PutLeave(DTOLVE02 objDTOLVE02)
        {
            Response response;
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
            return Ok(response);
        }

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
