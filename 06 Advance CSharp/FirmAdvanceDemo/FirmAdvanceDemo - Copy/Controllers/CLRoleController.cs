using FirmAdvanceDemo.BL;
using FirmAdvanceDemo.Models;
using FirmAdvanceDemo.Utitlity;
using Newtonsoft.Json.Linq;
using System.Net.Http;
using System.Web.Http;

namespace FirmAdvanceDemo.Controllers
{
    [RoutePrefix("api/role")]
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
        [Route("")]
        public IHttpActionResult GetRoles()
        {
            ResponseStatusInfo responseStatusInfo = BLResource<RLE01>.FetchResource();
            return this.Returner(responseStatusInfo);
        }

        [HttpGet]
        [Route("{id}")]
        public IHttpActionResult GetRole(int id)
        {
            ResponseStatusInfo responseStatusInfo = BLResource<RLE01>.FetchResource(id);
            return this.Returner(responseStatusInfo);
        }

        [HttpPost]
        [Route("")]
        public IHttpActionResult PostRole(RLE01 role)
        {
            ResponseStatusInfo responseStatusInfo = BLResource<RLE01>.AddResource(role);
            return this.Returner(responseStatusInfo);
        }

        [HttpPatch]
        [Route("{id}")]
        public IHttpActionResult PatchRole(int id, JObject toUpdateJson)
        {
            ResponseStatusInfo responseStatusInfo = BLResource<RLE01>.UpdateResource(id, toUpdateJson);
            return this.Returner(responseStatusInfo);
        }

        [HttpDelete]
        [Route("{id}")]
        public IHttpActionResult DeleteRole(int id)
        {
            ResponseStatusInfo responseStatusInfo = BLResource<RLE01>.RemoveResource(id);
            return this.Returner(responseStatusInfo);
        }
    }
}
