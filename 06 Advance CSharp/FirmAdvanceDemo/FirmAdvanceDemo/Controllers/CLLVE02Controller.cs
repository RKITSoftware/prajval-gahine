using FirmAdvanceDemo.BL;
using FirmAdvanceDemo.Enums;
using FirmAdvanceDemo.Models.POCO;
using FirmAdvanceDemo.Utitlity;
using Newtonsoft.Json.Linq;
using System;
using System.Web.Http;

namespace FirmAdvanceDemo.Controllers
{
    [RoutePrefix("api/leave")]
    public class CLLVE02Controller : BaseController
    {
        /// <summary>
        /// Instance of BLLeave
        /// </summary>
        private readonly BLLVE02Handler _objBLLeave;

        /// <summary>
        /// Default constructor for CLLeaveController
        /// </summary>
        public CLLVE02Controller()
        {
            _objBLLeave = new BLLVE02Handler();
        }



        [HttpGet]
        [Route("")]
        public IHttpActionResult GetAllLeaves()
        {
            Response response = _objBLLeave.FetchLeaves(LeaveStatus.N);
            return Ok(response);
        }

        [HttpGet]
        [Route("approved")]
        public IHttpActionResult GetApproveLeaves()
        {
            Response response = _objBLLeave.FetchLeaves(LeaveStatus.A);
            return Ok(response);
        }

        [HttpGet]
        [Route("pending")]
        public IHttpActionResult GetPendingLeaves()
        {
            Response response = _objBLLeave.FetchLeaves(LeaveStatus.P);
            return Ok(response);
        }

        [HttpGet]
        [Route("rejected")]
        public IHttpActionResult GetRejectedLeaves()
        {
            Response response = _objBLLeave.FetchLeaves(LeaveStatus.R);
            return Ok(response);
        }

        [HttpGet]
        [Route("employee/{id}")]
        public IHttpActionResult GetLeaveByEmployeeId(int id)
        {
            Response response = _objBLLeave.FetchLeaveByEmployeeId(id);
            return Ok(response);
        }

        [HttpGet]
        [Route("monthyear/{month}/{year}")]
        public IHttpActionResult GetLeaveByMonthYear(int month, int year)
        {
            Response response = _objBLLeave.FetchLeaveByMonthYear(month, year);
            return Ok(response);
        }

        [HttpGet]
        [Route("today")]
        public IHttpActionResult GetTodaysLeave()
        {
            Response response = _objBLLeave.FetchDateLeaves();
            return Ok(response);
        }

        [HttpGet]
        [Route("date/{date}")]
        public IHttpActionResult GetTodaysLeave(string date)
        {
            Response response = _objBLLeave.FetchDateLeaves(DateTime.ParseExact(date, "yyyy-MM-dd", null));
            return Ok(response);
        }

        [HttpGet]
        [Route("monthyear/{month}/{year}/employee/{id}")]
        public IHttpActionResult GetLeaveByEmployeeIdAndMonthYear(int id, int month, int year)
        {
            Response response = _objBLLeave.FetchLeaveByEmployeeIdAndMonthYear(id, month, year);
            return Ok(response);
        }

        [HttpGet]
        [Route("currentmonth/employee/{id}")]
        public IHttpActionResult GetLeaveByEmployeeIdForCurrentMonth(int id)
        {
            Response response = _objBLLeave.FetchLeaveByEmployeeIdForCurrentMonth(id);
            return Ok(response);
        }


        [HttpGet]
        [Route("employee/{id}/monthyear/{month}/{year}/count")]
        public IHttpActionResult GetLeaveCountByEmployeeIdAnMonthYear(int id, int month, int year)
        {
            Response response = _objBLLeave.GetLeaveCountByEmployeeIdAnMonthYear(id, month, year);
            return Ok(response);
        }


        //[HttpGet]
        //[Route("{id}")]
        //public IHttpActionResult GetLeave(int id)
        //{
        //    ResponseStatusInfo response = _objBLLeave.FetchResource(id);
        //    return Ok(response);
        //}

        [HttpPost]
        [Route("")]
        public IHttpActionResult PostLeave(LVE02 leave)
        {
            Response response = _objBLLeave.AddLeave(leave);
            return Ok(response);
        }

        [HttpPatch]
        [Route("{id}")]
        public IHttpActionResult PatchLeave(int id, JObject toUpdateJson)
        {
            Response response = _objBLLeave.UpdateResource(id, toUpdateJson);
            return Ok(response);
        }

        [HttpDelete]
        [Route("{id}")]
        public IHttpActionResult DeleteLeave(int id)
        {
            Response response = _objBLLeave.RemoveResource(id);
            return Ok(response);
        }

        [HttpPatch]
        [Route("approve/{leaveId}")]
        public IHttpActionResult PatchApproveLeave(int leaveId)
        {
            Response response = _objBLLeave.UpdateLeaveStatus(leaveId, LeaveStatus.A);
            return Ok(response);
        }

        [HttpPatch]
        [Route("reject/{leaveId}")]
        public IHttpActionResult PatchRejectLeave(int leaveId)
        {
            Response response = _objBLLeave.UpdateLeaveStatus(leaveId, LeaveStatus.R);
            return Ok(response);
        }

        [HttpPatch]
        [Route("expire")]
        public IHttpActionResult PatchExpire()
        {
            Response response = _objBLLeave.UpdateLeavesToExpire();
            return Ok(response);
        }
    }
}
