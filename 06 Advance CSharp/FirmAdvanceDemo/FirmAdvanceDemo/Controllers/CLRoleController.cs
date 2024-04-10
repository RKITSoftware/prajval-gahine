using FirmAdvanceDemo.BL;
using FirmAdvanceDemo.Models.POCO;
using FirmAdvanceDemo.Utitlity;
using Newtonsoft.Json.Linq;
using System.Web.Http;

namespace FirmAdvanceDemo.Controllers
{
    [RoutePrefix("api/role")]
    public class CLRoleController : BaseController
    {


        /// <summary>
        /// Instance of BLRole
        /// </summary>
        private readonly BLRole _objBLRole;

        /// <summary>
        /// Default constructor for CLRoleController
        /// </summary>
        public CLRoleController()
        {
            _objBLRole = new BLRole();
        }

        [HttpGet]
        [Route("")]
        public IHttpActionResult GetRoles()
        {
            ResponseStatusInfo responseStatusInfo = _objBLRole.FetchResource();
            return Returner(responseStatusInfo);
        }

        [HttpGet]
        [Route("{id}")]
        public IHttpActionResult GetRole(int id)
        {
            ResponseStatusInfo responseStatusInfo = _objBLRole.FetchResource(id);
            return Returner(responseStatusInfo);
        }

        [HttpPost]
        [Route("")]
        public IHttpActionResult PostRole(RLE01 role)
        {
            ResponseStatusInfo responseStatusInfo = _objBLRole.AddResource(role);
            return Returner(responseStatusInfo);
        }

        [HttpPatch]
        [Route("{id}")]
        public IHttpActionResult PatchRole(int id, JObject toUpdateJson)
        {
            ResponseStatusInfo responseStatusInfo = _objBLRole.UpdateResource(id, toUpdateJson);
            return Returner(responseStatusInfo);
        }

        [HttpDelete]
        [Route("{id}")]
        public IHttpActionResult DeleteRole(int id)
        {
            ResponseStatusInfo responseStatusInfo = _objBLRole.RemoveResource(id);
            return Returner(responseStatusInfo);
        }
    }
}
