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
    public class CLUSR01Controller : BaseController
    {
        /// <summary>
        /// Instance of BLUser
        /// </summary>
        private readonly BLUSR01Handler _objBLUser;

        /// <summary>
        /// Default constructor for CLUserController
        /// </summary>
        public CLUSR01Controller() : base()
        {
            _objBLUser = new BLUSR01Handler(_statusInfo);
        }

        [HttpGet]
        [Route("")]
        [BasicAuthorization(Roles = "admin")]
        public IHttpActionResult Get()
        {
            Response 
                = _objBLUser.RetrieveResource();
            

        }

        [HttpGet]
        [Route("{id}")]
        public IHttpActionResult Get(int id)
        {
            Response response = _objBLUser.FetchResource(id);
            return Ok(response);
        }

        [HttpPost]
        [Route("")]
        public IHttpActionResult Post(DTOUSR01 objDTOUSR01)
        {
            if (_objBLUser.Prevalidate(objDTOUSR01, EnmRole.A, EnmOperation.A))
            {
                _objBLUser.Presave(objDTOUSR01, EnmOperation.A);
                if (_objBLUser.Validate())
                {
                    _objBLUser.Save(EnmOperation.A, out _);
                }
            }
            return Returner();
        }

        [HttpPatch]
        [Route("{id}")]
        public IHttpActionResult Patch(int id, JObject toUpdateJson)
        {
            Response response = _objBLUser.UpdateResource(id, toUpdateJson);
            return Ok(response);
        }

        [HttpDelete]
        [Route("{id}")]
        public IHttpActionResult Delete(int id)
        {
            Response response = _objBLUser.RemoveResource(id);
            return Ok(response);
        }
    }
}
