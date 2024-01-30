using FirmAdvanceDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using FirmAdvanceDemo.BL;
using FirmAdvanceDemo.Utitlity;
using Newtonsoft.Json.Linq;

namespace FirmAdvanceDemo.Controllers
{
    [RoutePrefix("api/department")]
    public class CLDepartmentController : ApiController
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
        public IHttpActionResult GetDepartments()
        {
            ResponseStatusInfo responseStatusInfo = BLResource<DPT01>.FetchResource();
            return this.Returner(responseStatusInfo);
        }

        [HttpGet]
        [Route("{id}")]
        public IHttpActionResult GetDepartment(int id)
        {
            ResponseStatusInfo responseStatusInfo = BLResource<DPT01>.FetchResource(id);
            return this.Returner(responseStatusInfo);
        }

        [HttpPost]
        [Route("")]
        public IHttpActionResult PostDepartment(DPT01 Department)
        {
            ResponseStatusInfo responseStatusInfo = BLResource<DPT01>.AddResource(Department);
            return this.Returner(responseStatusInfo);
        }

        [HttpPatch]
        [Route("{id}")]
        public IHttpActionResult PatchDepartment(int id, JObject toUpdateJson)
        {
            ResponseStatusInfo responseStatusInfo = BLResource<DPT01>.UpdateResource(id, toUpdateJson);
            return this.Returner(responseStatusInfo);
        }

        [HttpDelete]
        [Route("{id}")]
        public IHttpActionResult DeleteDepartment(int id)
        {
            ResponseStatusInfo responseStatusInfo = BLResource<DPT01>.RemoveResource(id);
            return this.Returner(responseStatusInfo);
        }
    }
}
