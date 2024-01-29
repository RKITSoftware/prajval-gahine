using FirmAdvanceDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using FirmAdvanceDemo.BL;
using FirmAdvanceDemo.Utitlity;

namespace FirmAdvanceDemo.Controllers
{
    [RoutePrefix("role")]
    public class CLRoleController : ApiController
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
        public IHttpActionResult GetRoles()
        {
            ResponseStatusInfo responseStatusInfo = BLRole.GetRoles();
            return this.Returner(responseStatusInfo);
        }


        [HttpPost]
        public IHttpActionResult PostRole(RLE01 role)
        {
            ResponseStatusInfo responseStatusInfo = BLRole.AddRole(role);
            return this.Returner(responseStatusInfo);
        }

        [HttpDelete]
        [Route("{id}")]
        public IHttpActionResult DeleteRole(int RoleId)
        {
            ResponseStatusInfo responseStatusInfo = BLRole.RemoveRole(RoleId);
            return this.Returner(responseStatusInfo);
        }
    }
}
