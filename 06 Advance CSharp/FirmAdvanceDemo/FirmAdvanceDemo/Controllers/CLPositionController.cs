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
    [RoutePrefix("api/position")]
    public class CLPositionController : ApiController
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
        public IHttpActionResult GetPositions()
        {
            ResponseStatusInfo responseStatusInfo = BLResource<PSN01>.FetchResource();
            return this.Returner(responseStatusInfo);
        }

        [HttpGet]
        [Route("{id}")]
        public IHttpActionResult GetPosition(int id)
        {
            ResponseStatusInfo responseStatusInfo = BLResource<PSN01>.FetchResource(id);
            return this.Returner(responseStatusInfo);
        }

        [HttpPost]
        [Route("")]
        public IHttpActionResult PostPosition(PSN01 Position)
        {
            ResponseStatusInfo responseStatusInfo = BLResource<PSN01>.AddResource(Position);
            return this.Returner(responseStatusInfo);
        }

        [HttpPatch]
        [Route("{id}")]
        public IHttpActionResult PatchPosition(int id, JObject toUpdateJson)
        {
            ResponseStatusInfo responseStatusInfo = BLResource<PSN01>.UpdateResource(id, toUpdateJson);
            return this.Returner(responseStatusInfo);
        }

        [HttpDelete]
        [Route("{id}")]
        public IHttpActionResult DeletePosition(int id)
        {
            ResponseStatusInfo responseStatusInfo = BLResource<PSN01>.RemoveResource(id);
            return this.Returner(responseStatusInfo);
        }
    }
}
