using FirmAdvanceDemo.Auth;
using FirmAdvanceDemo.BL;
using FirmAdvanceDemo.Enums;
using FirmAdvanceDemo.Models.DTO;
using FirmAdvanceDemo.Utitlity;
using Newtonsoft.Json.Linq;
using System.Web.Http;

namespace FirmAdvanceDemo.Controllers
{
    [RoutePrefix("api/user")]
    //[AccessTokenAuthentication]
    public class CLUserController : BaseController
    {
        /// <summary>
        /// Instance of BLUser
        /// </summary>
        private readonly BLUser _objBLUser;

        /// <summary>
        /// Default constructor for CLUserController
        /// </summary>
        public CLUserController() : base()
        {
            _objBLUser = new BLUser(_statusInfo);
        }

        [HttpGet]
        [Route("")]
        [BasicAuthorization(Roles = "admin")]
        public IHttpActionResult GetUsers()
        {
            ResponseStatusInfo responseStatusInfo = _objBLUser.FetchResource();
            return Returner(responseStatusInfo);
        }

        [HttpGet]
        [Route("{id}")]
        public IHttpActionResult GetUser(int id)
        {
            ResponseStatusInfo responseStatusInfo = _objBLUser.FetchResource(id);
            return Returner(responseStatusInfo);
        }

        [HttpPost]
        [Route("")]
        public IHttpActionResult PostUser(DTOUSR01 objDTOUSR01)
        {
            if (_objBLUser.Prevalidate(objDTOUSR01, EnmRole.Admin, EnmDBOperation.Create))
            {
                _objBLUser.Presave(objDTOUSR01, EnmDBOperation.Create);
                if (_objBLUser.Validate())
                {
                    _objBLUser.Save(EnmDBOperation.Create, out _);
                }
            }
            return Returner();
        }

        [HttpPatch]
        [Route("{id}")]
        public IHttpActionResult PatchUser(int id, JObject toUpdateJson)
        {
            ResponseStatusInfo responseStatusInfo = _objBLUser.UpdateResource(id, toUpdateJson);
            return Returner(responseStatusInfo);
        }

        [HttpDelete]
        [Route("{id}")]
        public IHttpActionResult DeleteUser(int id)
        {
            ResponseStatusInfo responseStatusInfo = _objBLUser.RemoveResource(id);
            return Returner(responseStatusInfo);
        }
    }
}
