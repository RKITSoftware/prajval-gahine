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
    public class CLLeaveController : BaseController
    {
        /// <summary>
        /// Instance of BLLeave
        /// </summary>
        private readonly BLLeave _objBLLeave;

        /// <summary>
        /// Default constructor for CLLeaveController
        /// </summary>
        public CLLeaveController()
        {
            _objBLLeave = new BLLeave();
        }



        [HttpGet]
        [Route("")]
        public IHttpActionResult GetAllLeaves()
        {
            ResponseStatusInfo responseStatusInfo = _objBLLeave.FetchLeaves(LeaveStatus.None);
            return Returner(responseStatusInfo);
        }

        [HttpGet]
        [Route("approved")]
        public IHttpActionResult GetApproveLeaves()
        {
            ResponseStatusInfo responseStatusInfo = _objBLLeave.FetchLeaves(LeaveStatus.Approved);
            return Returner(responseStatusInfo);
        }

        [HttpGet]
        [Route("pending")]
        public IHttpActionResult GetPendingLeaves()
        {
            ResponseStatusInfo responseStatusInfo = _objBLLeave.FetchLeaves(LeaveStatus.Pending);
            return Returner(responseStatusInfo);
        }

        [HttpGet]
        [Route("rejected")]
        public IHttpActionResult GetRejectedLeaves()
        {
            ResponseStatusInfo responseStatusInfo = _objBLLeave.FetchLeaves(LeaveStatus.Rejected);
            return Returner(responseStatusInfo);
        }

        [HttpGet]
        [Route("employee/{id}")]
        public IHttpActionResult GetLeaveByEmployeeId(int id)
        {
            ResponseStatusInfo responseStatusInfo = _objBLLeave.FetchLeaveByEmployeeId(id);
            return Returner(responseStatusInfo);
        }

        [HttpGet]
        [Route("monthyear/{month}/{year}")]
        public IHttpActionResult GetLeaveByMonthYear(int month, int year)
        {
            ResponseStatusInfo responseStatusInfo = _objBLLeave.FetchLeaveByMonthYear(month, year);
            return Returner(responseStatusInfo);
        }

        [HttpGet]
        [Route("today")]
        public IHttpActionResult GetTodaysLeave()
        {
            ResponseStatusInfo responseStatusInfo = _objBLLeave.FetchDateLeaves();
            return Returner(responseStatusInfo);
        }

        [HttpGet]
        [Route("date/{date}")]
        public IHttpActionResult GetTodaysLeave(string date)
        {
            ResponseStatusInfo responseStatusInfo = _objBLLeave.FetchDateLeaves(DateTime.ParseExact(date, "yyyy-MM-dd", null));
            return Returner(responseStatusInfo);
        }

        [HttpGet]
        [Route("monthyear/{month}/{year}/employee/{id}")]
        public IHttpActionResult GetLeaveByEmployeeIdAndMonthYear(int id, int month, int year)
        {
            ResponseStatusInfo responseStatusInfo = _objBLLeave.FetchLeaveByEmployeeIdAndMonthYear(id, month, year);
            return Returner(responseStatusInfo);
        }

        [HttpGet]
        [Route("currentmonth/employee/{id}")]
        public IHttpActionResult GetLeaveByEmployeeIdForCurrentMonth(int id)
        {
            ResponseStatusInfo responseStatusInfo = _objBLLeave.FetchLeaveByEmployeeIdForCurrentMonth(id);
            return Returner(responseStatusInfo);
        }


        [HttpGet]
        [Route("employee/{id}/monthyear/{month}/{year}/count")]
        public IHttpActionResult GetLeaveCountByEmployeeIdAnMonthYear(int id, int month, int year)
        {
            ResponseStatusInfo responseStatusInfo = _objBLLeave.GetLeaveCountByEmployeeIdAnMonthYear(id, month, year);
            return Returner(responseStatusInfo);
        }


        //[HttpGet]
        //[Route("{id}")]
        //public IHttpActionResult GetLeave(int id)
        //{
        //    ResponseStatusInfo responseStatusInfo = _objBLLeave.FetchResource(id);
        //    return Returner(responseStatusInfo);
        //}

        [HttpPost]
        [Route("")]
        public IHttpActionResult PostLeave(LVE02 leave)
        {
            ResponseStatusInfo responseStatusInfo = _objBLLeave.AddLeave(leave);
            return Returner(responseStatusInfo);
        }

        [HttpPatch]
        [Route("{id}")]
        public IHttpActionResult PatchLeave(int id, JObject toUpdateJson)
        {
            ResponseStatusInfo responseStatusInfo = _objBLLeave.UpdateResource(id, toUpdateJson);
            return Returner(responseStatusInfo);
        }

        [HttpDelete]
        [Route("{id}")]
        public IHttpActionResult DeleteLeave(int id)
        {
            ResponseStatusInfo responseStatusInfo = _objBLLeave.RemoveResource(id);
            return Returner(responseStatusInfo);
        }

        [HttpPatch]
        [Route("approve/{leaveId}")]
        public IHttpActionResult PatchApproveLeave(int leaveId)
        {
            ResponseStatusInfo responseStatusInfo = _objBLLeave.UpdateLeaveStatus(leaveId, LeaveStatus.Approved);
            return Returner(responseStatusInfo);
        }

        [HttpPatch]
        [Route("reject/{leaveId}")]
        public IHttpActionResult PatchRejectLeave(int leaveId)
        {
            ResponseStatusInfo responseStatusInfo = _objBLLeave.UpdateLeaveStatus(leaveId, LeaveStatus.Rejected);
            return Returner(responseStatusInfo);
        }

        [HttpPatch]
        [Route("expire")]
        public IHttpActionResult PatchExpire()
        {
            ResponseStatusInfo responseStatusInfo = _objBLLeave.UpdateLeavesToExpire();
            return Returner(responseStatusInfo);
        }
    }
}
