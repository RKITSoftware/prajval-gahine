using FirmAdvanceDemo.Auth;
using FirmAdvanceDemo.BL;
using FirmAdvanceDemo.Enums;
using FirmAdvanceDemo.Models.DTO;
using FirmAdvanceDemo.Utitlity;
using Newtonsoft.Json.Linq;
using System.Web.Http;
using System.Web.Security;

namespace FirmAdvanceDemo.Controllers
{
    [RoutePrefix("api/user")]
    //[AccessTokenAuthentication]
    public class CLUSR01Controller : ApiController
    {
        /// <summary>
        /// Instance of BLUser
        /// </summary>
        private readonly BLUSR01Handler _objBLUSR01Handler;

        /// <summary>
        /// Default constructor for CLUserController
        /// </summary>
        public CLUSR01Controller()
        {
            _objBLUSR01Handler = new BLUSR01Handler();
        }

        [HttpGet]
        [Route("")]
        [BasicAuthorization(Roles = "admin")]
        public IHttpActionResult Get()
        {
            Response response = _objBLUSR01Handler.RetrieveUser();
            return Ok(response);
        }

        [HttpGet]
        [Route("{id}")]
        public IHttpActionResult Get(int id)
        {
            Response response = _objBLUSR01Handler.RetrieveUser(id);
            return Ok(response);
        }

        [HttpPost]
        [Route("")]
        public IHttpActionResult Post(DTOUSR01 objDTOUSR01)
        {
            Response response;
            _objBLUSR01Handler.Operation = EnmOperation.A;
            response = _objBLUSR01Handler.Prevalidate(objDTOUSR01);
            if (!response.IsError)
            {
                _objBLUSR01Handler.Presave(objDTOUSR01);
                response = _objBLUSR01Handler.Validate();
                if (!response.IsError)
                {
                    _objBLUSR01Handler.Save();
                }
            }
            return Ok();
        }

        [HttpPatch]
        [Route("{id}")]
        public IHttpActionResult Patch(DTOUSR01 objDTOUSR01)
        {
            Response response;
            _objBLUSR01Handler.Operation = EnmOperation.E;
            response = _objBLUSR01Handler.Prevalidate(objDTOUSR01);
            if (!response.IsError)
            {
                _objBLUSR01Handler.Presave(objDTOUSR01);
                response = _objBLUSR01Handler.Validate();
                if (!response.IsError)
                {
                    _objBLUSR01Handler.Save();
                }
            }
            return Ok();
        }

        [HttpDelete]
        [Route("{userID}")]
        public IHttpActionResult Delete(int userID)
        {
            Response response = _objBLUSR01Handler.ValidateDelete(userID);
            if (!response.IsError)
            {
                response = _objBLUSR01Handler.Delete(userID);
            }
            return Ok(response);
        }
    }
}
