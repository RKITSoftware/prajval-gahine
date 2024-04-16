using FirmAdvanceDemo.Auth;
using FirmAdvanceDemo.BL;
using FirmAdvanceDemo.Models.POCO;
using FirmAdvanceDemo.Utitlity;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;

namespace FirmAdvanceDemo.Controllers
{
    [RoutePrefix("api/punch")]
    [AccessTokenAuthentication]
    [BasicAuthorization(Roles = "employee")]
    public class CLPCH01Controller : BaseController
    {
        /// <summary>
        /// Method used to have consistent (uniform) returns from all controller actions
        /// </summary>
        /// <param name="">ResponseStatusInfo instance containing response specific information</param>
        /// <returns>Instance of type IHttpActionResult</returns>

        /// <summary>
        /// Instance of BLPunch
        /// </summary>
        private readonly BLPCH01Handler _objBLPunch;

        /// <summary>
        /// Default constructor for CLPunchController
        /// </summary>
        public CLPCH01Controller()
        {
            _objBLPunch = new BLPCH01Handler();
        }

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

            if (EmployeeId == 0)
            {
                return ResponseMessage(Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Not an employee"));
            }

            PCH01 punch = new PCH01()
            {
                t01f01 = 0,
                h01f02 = EmployeeId,
                h01f03 = DateTime.Now
            };
            _objBLPunch.AddResource(punch);

            return Ok(ResponseWrapper.Wrap("Punched Successfully", null));
        }

        [HttpGet]
        [Route("generate-attendance")]
        [AccessTokenAuthentication]
        [BasicAuthorization(Roles = "admin")]
        public IHttpActionResult GenerateAttendance()
        {
            Response statusInfo = _objBLPunch.GenerateAttendance(DateTime.Now);
            return Returner(statusInfo);
        }
    }
}
