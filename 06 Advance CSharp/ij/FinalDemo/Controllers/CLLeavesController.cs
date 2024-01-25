using FinalDemo.Models;
using FinalDemo.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace FinalDemo.Controllers
{
    [RoutePrefix("api")]
    public class CLLeavesController : ApiController
    {
        //static BLLeave _leaves;

        //static CLLeavesController()
        //{
        //    _leaves = new BLLeave();
        //}

        [HttpGet,Route("GetAllLeave")]
        public IHttpActionResult GetAllLeave()
        {
            return Ok(BLLeave.GetAllLeave());
        }

        [HttpGet, Route("GetLeaveRequestById/{id}")]
        public IHttpActionResult GetLeaveRequestById(int id)
        {
            return Ok(BLLeave.GetLeaveRequestById(id));
        }

        [HttpPost,Route("SubmitLeaveRequest")]
        public IHttpActionResult SubmitLeaveRequest(LeaveRequest objLeave)
        {
            return Ok(BLLeave.SubmitLeaveRequest(objLeave));
        }

        
        [HttpPut]
        [Route("{requestId}/status")]
        public IHttpActionResult ApproveRejectLeaveRequest(int requestId, [FromBody] string newStatus)
        {
             return Ok(BLLeave.ApproveRejectLeaveRequest(requestId,newStatus));
        }
    }
}
