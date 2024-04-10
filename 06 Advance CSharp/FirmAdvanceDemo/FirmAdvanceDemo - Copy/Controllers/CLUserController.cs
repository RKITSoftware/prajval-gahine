using FirmAdvanceDemo.Auth;
using FirmAdvanceDemo.BL;
using FirmAdvanceDemo.Utitlity;
using Newtonsoft.Json.Linq;
using System.Net.Http;
using System.Web.Http;

namespace FirmAdvanceDemo.Controllers
{
    [RoutePrefix("api/user")]
    [AccessTokenAuthentication]
    public class CLUserController : ApiController
    {
        /// <summary>
        /// Instance of BLUser class
        /// </summary>
        private readonly BLUser _objBLUser;

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
        [BasicAuthorization(Roles = "admin")]
        public IHttpActionResult GetUsers()
        {
            ResponseStatusInfo responseStatusInfo = _objBLUser.FetchResource();
            return this.Returner(responseStatusInfo);
        }

        [HttpGet]
        [Route("{id}")]
        public IHttpActionResult GetUser(int id)
        {
            ResponseStatusInfo responseStatusInfo = _objBLUser.FetchResource(id);
            return this.Returner(responseStatusInfo);
        }

        [HttpPost]
        [Route("")]
        public IHttpActionResult PostUser(JObject UserEmployeeJson)
        {
            ResponseStatusInfo responseStatusInfo = _objBLUser.AddResource(UserEmployeeJson);
            return this.Returner(responseStatusInfo);
        }

        [HttpPatch]
        [Route("{id}")]
        public IHttpActionResult PatchUser(int id, JObject toUpdateJson)
        {
            ResponseStatusInfo responseStatusInfo = _objBLUser.UpdateResource(id, toUpdateJson);
            return this.Returner(responseStatusInfo);
        }

        [HttpDelete]
        [Route("{id}")]
        public IHttpActionResult DeleteUser(int id)
        {
            ResponseStatusInfo responseStatusInfo =_objBLUser.RemoveResource(id);
            return this.Returner(responseStatusInfo);
        }
    }
}
