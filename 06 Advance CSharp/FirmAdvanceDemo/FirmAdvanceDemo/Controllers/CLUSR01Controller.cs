using FirmAdvanceDemo.Auth;
using FirmAdvanceDemo.BL;
using FirmAdvanceDemo.Enums;
using FirmAdvanceDemo.Models.DTO;
using FirmAdvanceDemo.Utility;
using System.Web.Http;

namespace FirmAdvanceDemo.Controllers
{
    /// <summary>
    /// Controller for managing user operations, such as retrieving, creating, updating, and deleting users.
    /// </summary>
    [RoutePrefix("api/user")]
    public class CLUSR01Controller : ApiController
    {
        /// <summary>
        /// Instance of the user handler for managing user-related operations.
        /// </summary>
        private readonly BLUSR01Handler _objBLUSR01Handler;

        /// <summary>
        /// Initializes a new instance of the CLUSR01Controller class.
        /// </summary>
        public CLUSR01Controller()
        {
            _objBLUSR01Handler = new BLUSR01Handler();
        }

        /// <summary>
        /// Retrieves all users.
        /// </summary>
        /// <returns>HTTP response containing the retrieved users.</returns>
        [HttpGet]
        [Route("")]
        [AccessTokenAuthentication]
        [BasicAuthorization(Roles = "A")]
        public IHttpActionResult Get()
        {
            Response response = _objBLUSR01Handler.RetrieveUser();
            return Ok(response);
        }

        /// <summary>
        /// Retrieves a user by their ID.
        /// </summary>
        /// <param name="userID">The ID of the user to retrieve.</param>
        /// <returns>HTTP response containing the retrieved user.</returns>
        [HttpGet]
        [Route("{userID}")]
        [AccessTokenAuthentication]
        [BasicAuthorization(Roles = "A")]
        public IHttpActionResult Get(int userID)
        {
            Response response = _objBLUSR01Handler.RetrieveUser(userID);
            return Ok(response);
        }

        /// <summary>
        /// Creates a new user.
        /// </summary>
        /// <param name="objDTOUSR01">The DTO representing the user to be created.</param>
        /// <returns>HTTP response indicating the success or failure of the operation.</returns>
        [HttpPost]
        [Route("")]
        [AccessTokenAuthentication]
        [BasicAuthorization(Roles = "A")]
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

        /// <summary>
        /// Updates an existing user.
        /// </summary>
        /// <param name="objDTOUSR01">The DTO representing the updated user data.</param>
        /// <returns>HTTP response indicating the success or failure of the operation.</returns>
        [HttpPatch]
        [Route("")]
        [AccessTokenAuthentication]
        [BasicAuthorization(Roles = "A")]
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
                    response = _objBLUSR01Handler.Save();
                }
            }
            return Ok(response);
        }

        /// <summary>
        /// Deletes a user by their ID.
        /// </summary>
        /// <param name="userID">The ID of the user to delete.</param>
        /// <returns>HTTP response indicating the success or failure of the operation.</returns>
        [HttpDelete]
        [Route("{userID}")]
        [AccessTokenAuthentication]
        [BasicAuthorization(Roles = "A")]
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
