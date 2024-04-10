using FirmAdvanceDemo.BL;
using FirmAdvanceDemo.Models;
using FirmAdvanceDemo.Utitlity;
using Newtonsoft.Json.Linq;
using System;
using System.Net.Http;
using System.Web.Http;
using System.Web.Services.Protocols;

namespace FirmAdvanceDemo.Controllers
{
    [RoutePrefix("api/leave")]
    public class CLLeaveController : ApiController
    {
        [NonAction]
        public IHttpActionResult Returner(ResponseStatusInfo responseStatusInfo)
        {
            if (responseStatusInfo.IsRequestSuccessful)
            {
                return Ok(ResponseWrapper.Wrap(responseStatusInfo.Message, responseStatusInfo.Data));
            }
            return ResponseMessage(Request.CreateErrorResponse(responseStatusInfo.StatusCode, responseStatusInfo.Message));
        }


        [HttpGet]
        [Route("")]
        public IHttpActionResult GetAllLeaves()
        {
            ResponseStatusInfo responseStatusInfo = BLLeave.FetchLeaves(LeaveStatus.None);
            return this.Returner(responseStatusInfo);
        }

        [HttpGet]
        [Route("approved")]
        public IHttpActionResult GetApproveLeaves()
        {
            ResponseStatusInfo responseStatusInfo = BLLeave.FetchLeaves(LeaveStatus.Approved);
            return this.Returner(responseStatusInfo);
        }

        [HttpGet]
        [Route("pending")]
        public IHttpActionResult GetPendingLeaves()
        {
            ResponseStatusInfo responseStatusInfo = BLLeave.FetchLeaves(LeaveStatus.Pending);
            return this.Returner(responseStatusInfo);
        }

        [HttpGet]
        [Route("rejected")]
        public IHttpActionResult GetRejectedLeaves()
        {
            ResponseStatusInfo responseStatusInfo = BLLeave.FetchLeaves(LeaveStatus.Rejected);
            return this.Returner(responseStatusInfo);
        }

        [HttpGet]
        [Route("employee/{id}")]
        public IHttpActionResult GetLeaveByEmployeeId(int id)
        {
            ResponseStatusInfo responseStatusInfo = BLLeave.FetchLeaveByEmployeeId(id);
            return this.Returner(responseStatusInfo);
        }

        [HttpGet]
        [Route("monthyear/{month}/{year}")]
        public IHttpActionResult GetLeaveByMonthYear(int month, int year)
        {
            ResponseStatusInfo responseStatusInfo = BLLeave.FetchLeaveByMonthYear(month, year);
            return this.Returner(responseStatusInfo);
        }

        [HttpGet]
        [Route("today")]
        public IHttpActionResult GetTodaysLeave()
        {
            ResponseStatusInfo responseStatusInfo = BLLeave.FetchDateLeaves();
            return this.Returner(responseStatusInfo);
        }

        [HttpGet]
        [Route("date/{date}")]
        public IHttpActionResult GetTodaysLeave(string date)
        {
            ResponseStatusInfo responseStatusInfo = BLLeave.FetchDateLeaves(DateTime.ParseExact(date, "yyyy-MM-dd", null));
            return this.Returner(responseStatusInfo);
        }

        [HttpGet]
        [Route("monthyear/{month}/{year}/employee/{id}")]
        public IHttpActionResult GetLeaveByEmployeeIdAndMonthYear(int id, int month, int year)
        {
            ResponseStatusInfo responseStatusInfo = BLLeave.FetchLeaveByEmployeeIdAndMonthYear(id, month, year);
            return this.Returner(responseStatusInfo);
        }

        [HttpGet]
        [Route("currentmonth/employee/{id}")]
        public IHttpActionResult GetLeaveByEmployeeIdForCurrentMonth(int id)
        {
            ResponseStatusInfo responseStatusInfo = BLLeave.FetchLeaveByEmployeeIdForCurrentMonth(id);
            return this.Returner(responseStatusInfo);
        }


        [HttpGet]
        [Route("employee/{id}/monthyear/{month}/{year}/count")]
        public IHttpActionResult GetLeaveCountByEmployeeIdAnMonthYear(int id, int month, int year)
        {
            ResponseStatusInfo responseStatusInfo = BLLeave.GetLeaveCountByEmployeeIdAnMonthYear(id, month, year);
            return this.Returner(responseStatusInfo);
        }


        //[HttpGet]
        //[Route("{id}")]
        //public IHttpActionResult GetLeave(int id)
        //{
        //    ResponseStatusInfo responseStatusInfo = BLResource<LVE01>.FetchResource(id);
        //    return this.Returner(responseStatusInfo);
        //}

        [HttpPost]
        [Route("")]
        public IHttpActionResult PostLeave(LVE01 leave)
        {
            ResponseStatusInfo responseStatusInfo = BLLeave.AddLeave(leave);
            return this.Returner(responseStatusInfo);
        }

        [HttpPatch]
        [Route("{id}")]
        public IHttpActionResult PatchLeave(int id, JObject toUpdateJson)
        {
            ResponseStatusInfo responseStatusInfo = BLResource<LVE01>.UpdateResource(id, toUpdateJson);
            return this.Returner(responseStatusInfo);
        }

        [HttpDelete]
        [Route("{id}")]
        public IHttpActionResult DeleteLeave(int id)
        {
            ResponseStatusInfo responseStatusInfo = BLResource<LVE01>.RemoveResource(id);
            return this.Returner(responseStatusInfo);
        }

        [HttpPatch]
        [Route("approve/{leaveId}")]
        public IHttpActionResult PatchApproveLeave(int leaveId)
        {
            ResponseStatusInfo responseStatusInfo = BLLeave.UpdateLeaveStatus(leaveId, LeaveStatus.Approved);
            return this.Returner(responseStatusInfo);
        }

        [HttpPatch]
        [Route("reject/{leaveId}")]
        public IHttpActionResult PatchRejectLeave(int leaveId)
        {
            ResponseStatusInfo responseStatusInfo = BLLeave.UpdateLeaveStatus(leaveId, LeaveStatus.Rejected);
            return this.Returner(responseStatusInfo);
        }

        [HttpPatch]
        [Route("expire")]
        public IHttpActionResult PatchExpire()
        {
            ResponseStatusInfo responseStatusInfo = BLLeave.UpdateLeavesToExpire();
            return this.Returner(responseStatusInfo);
        }
    }
}
