using FirmAdvanceDemo.Auth;
using FirmAdvanceDemo.BL;
using FirmAdvanceDemo.Enums;
using FirmAdvanceDemo.Models.DTO;
using FirmAdvanceDemo.Utility;
using System.Web.Http;

namespace FirmAdvanceDemo.Controllers
{
    /// <summary>
    /// Controller for managing role operations, such as retrieving, adding, updating, and deleting roles.
    /// </summary>
    [RoutePrefix("api/role")]
    public class CLRLE01Controller : ApiController
    {
        /// <summary>
        /// Instance of the role handler for managing role operations.
        /// </summary>
        private readonly BLRLE01Handler _objBLRLE01Handler;

        /// <summary>
        /// Initializes a new instance of the CLRLE01Controller class.
        /// </summary>
        public CLRLE01Controller()
        {
            _objBLRLE01Handler = new BLRLE01Handler();
        }

        /// <summary>
        /// Retrieves all roles.
        /// </summary>
        /// <returns>HTTP response containing the retrieved roles.</returns>
        [HttpGet]
        [Route("")]
        [AccessTokenAuthentication]
        [BasicAuthorization(Roles = "A")]
        public IHttpActionResult GetRole()
        {
            Response response = _objBLRLE01Handler.RetrieveRole();
            return Ok(response);
        }

        /// <summary>
        /// Retrieves a specific role by its ID.
        /// </summary>
        /// <param name="id">The ID of the role to retrieve.</param>
        /// <returns>HTTP response containing the retrieved role.</returns>
        [HttpGet]
        [Route("{roleID}")]
        [AccessTokenAuthentication]
        [BasicAuthorization(Roles = "A")]
        public IHttpActionResult GetRole(int roleID)
        {
            Response response = _objBLRLE01Handler.RetrieveRole(roleID);
            return Ok(response);
        }

        /// <summary>
        /// Adds a new role.
        /// </summary>
        /// <param name="objDTORLE01">DTO containing the details of the role to add.</param>
        /// <returns>HTTP response indicating the success or failure of the operation.</returns>
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

        /// <summary>
        /// Updates an existing role.
        /// </summary>
        /// <param name="objDTORLE01">DTO containing the updated details of the role.</param>
        /// <returns>HTTP response indicating the success or failure of the operation.</returns>
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

        /// <summary>
        /// Deletes a role by its ID.
        /// </summary>
        /// <param name="roleID">The ID of the role to delete.</param>
        /// <returns>HTTP response indicating the success or failure of the operation.</returns>
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
