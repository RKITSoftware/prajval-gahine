using FirmAdvanceDemo.BL;
using FirmAdvanceDemo.Enums;
using FirmAdvanceDemo.Models.DTO;
using FirmAdvanceDemo.Utitlity;
using System;
using System.Web.Http;

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
        public IHttpActionResult GetLeave()
        {
            Response response = _objBLLVE02Handler.RetrieveLeave();
            return Ok(response);
        }

        [HttpGet]
        [Route("{leaveId}")]
        public IHttpActionResult GetLeave(int leaveId)
        {
            Response response = _objBLLVE02Handler.RetrieveLeave(leaveId);
            return Ok(response);
        }

        [HttpGet]
        [Route("status/{leaveStatus}")]
        public IHttpActionResult GetLeaveByStatus(EnmLeaveStatus leaveStatus)
        {
            Response response = _objBLLVE02Handler.RetrieveLeaveByStatus(leaveStatus);
            return Ok(response);
        }

        [HttpGet]
        [Route("employee/{employeeId}")]
        public IHttpActionResult GetLeaveByEmployee(int employeeId)
        {
            Response response = _objBLLVE02Handler.RetrieveLeaveByEmployee(employeeId);
            return Ok(response);
        }

        [HttpGet]
        [Route("employee/{employeeId}")]
        public IHttpActionResult GetLeaveByEmployeeAndMonthYear(int employeeId, int year, int month)
        {
            Response response = _objBLLVE02Handler.RetrieveLeaveByEmployeeAndMonthYear(employeeId, year, month);
            return Ok(response);
        }

        [HttpGet]
        [Route("employee/{employeeId}")]
        public IHttpActionResult GetLeaveByEmployeeAndYear(int employeeId, int year)
        {
            Response response = _objBLLVE02Handler.RetrieveLeaveByEmployeeAnYear(employeeId, year);
            return Ok(response);
        }

        [HttpGet]
        [Route("employee/{employeeId}/current-year")]
        public IHttpActionResult GetLeaveByEmployeeForCurrentYear(int employeeId)
        {
            Response response = _objBLLVE02Handler.RetrieveLeaveByEmployeeAnYear(employeeId, DateTime.Now.Year);
            return Ok(response);
        }

        [HttpGet]
        [Route("employee/{employeeId}/current-month")]
        public IHttpActionResult GetLeaveByEmployeeForCurrentMonth(int employeeId)
        {
            DateTime now = DateTime.Now;
            Response response = _objBLLVE02Handler.RetrieveLeaveByEmployeeAndMonthYear(employeeId, now.Year, now.Month);
            return Ok(response);
        }

        [HttpGet]
        [Route("monthly")]
        public IHttpActionResult GetLeaveByMonthYear(int year, int month)
        {
            Response response = _objBLLVE02Handler.RetrieveLeaveByMonthYear(year, month);
            return Ok(response);
        }

        [HttpGet]
        [Route("date/{date}")]
        public IHttpActionResult GetLeaveByDate(DateTime date)
        {
            Response response = _objBLLVE02Handler.RetrieveLeaveByDate(DateTime.ParseExact(date, "yyyy-MM-dd", null));
            return Ok(response);
        }

        [HttpGet]
        [Route("today")]
        public IHttpActionResult GetLeaveForToday()
        {
            Response response = _objBLLVE02Handler.RetrieveLeaveByDate(DateTime.Now.Date);
            return Ok(response);
        }

        [HttpPost]
        [Route("")]
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
