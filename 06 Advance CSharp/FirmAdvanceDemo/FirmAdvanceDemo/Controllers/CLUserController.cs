using FirmAdvanceDemo.BL;
using FirmAdvanceDemo.Models;
using FirmAdvanceDemo.Utitlity;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace FirmAdvanceDemo.Controllers
{
    [RoutePrefix("api/user")]
    public class CLUserController : ApiController
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
        public IHttpActionResult GetUsers()
        {
            ResponseStatusInfo responseStatusInfo = BLResource<USR01>.FetchResource();
            return this.Returner(responseStatusInfo);
        }

        [HttpGet]
        [Route("{id}")]
        public IHttpActionResult GetUser(int id)
        {
            ResponseStatusInfo responseStatusInfo = BLResource<USR01>.FetchResource(id);
            return this.Returner(responseStatusInfo);
        }

        [HttpPost]
        [Route("")]
        public IHttpActionResult PostUser(JObject UserEmployeeJson)
        {
            ResponseStatusInfo responseStatusInfo = BLUser.AddResource(UserEmployeeJson);
            return this.Returner(responseStatusInfo);
        }

        [HttpPatch]
        [Route("{id}")]
        public IHttpActionResult PatchUser(int id, JObject toUpdateJson)
        {
            ResponseStatusInfo responseStatusInfo = BLResource<USR01>.UpdateResource(id, toUpdateJson);
            return this.Returner(responseStatusInfo);
        }

        [HttpDelete]
        [Route("{id}")]
        public IHttpActionResult DeleteUser(int id)
        {
            ResponseStatusInfo responseStatusInfo = BLResource<USR01>.RemoveResource(id);
            return this.Returner(responseStatusInfo);
        }
    }
}
