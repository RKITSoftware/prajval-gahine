using FirmAdvanceDemo.BL;
using FirmAdvanceDemo.Models.POCO;
using FirmAdvanceDemo.Utitlity;
using Newtonsoft.Json.Linq;
using System.Web.Http;

namespace FirmAdvanceDemo.Controllers
{
    [RoutePrefix("api/role")]
    public class CLRLE01Controller
    {


        /// <summary>
        /// Instance of BLRole
        /// </summary>
        private readonly BLRLE01Handler _objBLRole;

        /// <summary>
        /// Default constructor for CLRoleController
        /// </summary>
        public CLRLE01Controller()
        {
            _objBLRole = new BLRLE01Handler();
        }

        [HttpGet]
        [Route("")]
        public IHttpActionResult GetRoles()
        {
            Response







                = _objBLRole.RetrieveResource();
            return Ok(response);
        }

        [HttpGet]
        [Route("{id}")]
        public IHttpActionResult GetRole(int id)
        {
            Response response = _objBLRole.FetchResource(id);
            return Ok(response);
        }

        [HttpPost]
        [Route("")]
        public IHttpActionResult PostRole(RLE01 role)
        {
            Response response = _objBLRole.AddResource(role);
            return Ok(response);
        }

        [HttpPatch]
        [Route("{id}")]
        public IHttpActionResult PatchRole(int id, JObject toUpdateJson)
        {
            Response response = _objBLRole.UpdateResource(id, toUpdateJson);
            return Ok(response);
        }

        [HttpDelete]
        [Route("{id}")]
        public IHttpActionResult DeleteRole(int id)
        {
            Response response = _objBLRole.RemoveResource(id);
            return Ok(response);
        }
    }
}
