using FirmAdvanceDemo.Auth;
using FirmAdvanceDemo.BL;
using FirmAdvanceDemo.Models;
using FirmAdvanceDemo.Utitlity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web;
using System.Web.Http;

namespace FirmAdvanceDemo.Controllers
{
    [RoutePrefix("api/punch")]
    [AccessTokenAuthentication]
    [BasicAuthorization(Roles = "employee")]
    public class CLPunchController : ApiController
    {
        [HttpPost]
        [Route("")]
        public IHttpActionResult PostPunch()
        {
            ClaimsIdentity identity = (ClaimsIdentity)User.Identity;
            int EmployeeId = int.Parse(
                identity.Claims.Where(c => c.Type == "EmployeeId")
                .Select(c => c.Value)
                .SingleOrDefault()
            );

            if(EmployeeId == 0)
            {
                return ResponseMessage(Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Not an employee"));
            }

            PCH01 punch = new PCH01()
            {
                Id = 0,
                h01f02 = EmployeeId,
                h01f03 = DateTime.Now
            };
            BLPunch.AddResource(punch);

            return Ok(ResponseWrapper.Wrap("Punched Successfully", null));
        }
    }
}