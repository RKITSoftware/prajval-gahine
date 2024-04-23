using FirmAdvanceDemo.Auth;
using FirmAdvanceDemo.BL;
using FirmAdvanceDemo.Enums;
using FirmAdvanceDemo.Models.DTO;
using FirmAdvanceDemo.Utitlity;
using System.Web.Http;

namespace FirmAdvanceDemo.Controllers
{
    [RoutePrefix("api/role")]
    public class CLRLE01Controller : ApiController
    {
        /// <summary>
        /// Instance of BLRole
        /// </summary>
        private readonly BLRLE01Handler _objBLRLE01Handler;

        /// <summary>
        /// Default constructor for CLRoleController
        /// </summary>
        public CLRLE01Controller()
        {
            _objBLRLE01Handler = new BLRLE01Handler();
        }

        [HttpGet]
        [Route("")]
        [AccessTokenAuthentication]
        [BasicAuthorization(Roles = "A")]
        public IHttpActionResult GetRole()
        {
            Response response = _objBLRLE01Handler.RetrieveRole();
            return Ok(response);
        }

        [HttpGet]
        [Route("{id}")]
        [AccessTokenAuthentication]
        [BasicAuthorization(Roles = "A")]
        public IHttpActionResult GetRole(int id)
        {
            Response response = _objBLRLE01Handler.RetrieveRole(id);
            return Ok(response);
        }

        [HttpPost]
        [Route("")]
        [AccessTokenAuthentication]
        [BasicAuthorization(Roles = "A")]
        public IHttpActionResult PostRole(DTORLE01 objDTORLE01)
        {
            Response response;

            _objBLRLE01Handler.Operation = EnmOperation.A;

            response = _objBLRLE01Handler.Prevalidate(objDTORLE01);

            if (!response.IsError)
            {
                _objBLRLE01Handler.Presave(objDTORLE01);
                response = _objBLRLE01Handler.Validate();
                if (!response.IsError)
                {
                    response = _objBLRLE01Handler.Save();
                }
            }
            return Ok(response);
        }

        [HttpPatch]
        [Route("")]
        [AccessTokenAuthentication]
        [BasicAuthorization(Roles = "A")]
        public IHttpActionResult PatchRole(DTORLE01 objDTORLE01)
        {
            Response response;

            _objBLRLE01Handler.Operation = EnmOperation.E;

            response = _objBLRLE01Handler.Prevalidate(objDTORLE01);

            if (!response.IsError)
            {
                _objBLRLE01Handler.Presave(objDTORLE01);
                response = _objBLRLE01Handler.Validate();
                if (!response.IsError)
                {
                    response = _objBLRLE01Handler.Save();
                }
            }
            return Ok(response);
        }

        [HttpDelete]
        [Route("{roleID}")]
        [AccessTokenAuthentication]
        [BasicAuthorization(Roles = "A")]
        public IHttpActionResult DeleteRole(int roleID)
        {
            Response response = _objBLRLE01Handler.ValidateDelete(roleID);
            if (!response.IsError)
            {
                response = _objBLRLE01Handler.Delete(roleID);
            }
            return Ok(response);
        }
    }
}
