using FirmAdvanceDemo.Auth;
using FirmAdvanceDemo.BL;
using FirmAdvanceDemo.Enums;
using FirmAdvanceDemo.Models.DTO;
using FirmAdvanceDemo.Utility;
using System.Web.Http;

namespace FirmAdvanceDemo.Controllers
{
    [Route("api/admin")]
    public class CLADM00Controller : ApiController
    {

        private readonly BLADM00Handler _objBLADM00Handler;

        public CLADM00Controller()
        {
            _objBLADM00Handler = new BLADM00Handler();
        }

        [HttpPost]
        [Route("")]
        [AccessTokenAuthentication]
        [BasicAuthorization(Roles = "A")]
        public IHttpActionResult PostAdmin(DTOUSR01 objDTOUSR01)
        {
            Response response;
            _objBLADM00Handler.Operation = EnmOperation.A;
            response = _objBLADM00Handler.PrevalidateUser(objDTOUSR01);
            if (!response.IsError)
            {
                _objBLADM00Handler.PresaveUser(objDTOUSR01);
                response = _objBLADM00Handler.Save();
            }
            return Ok(response);
        }

        [HttpPut]
        [Route("")]
        [AccessTokenAuthentication]
        [BasicAuthorization(Roles = "A")]
        public IHttpActionResult PutAdmin(DTOUSR01 objDTOUSR01)
        {
            Response response;
            _objBLADM00Handler.Operation = EnmOperation.E;
            response = _objBLADM00Handler.PrevalidateUser(objDTOUSR01);
            if (!response.IsError)
            {
                _objBLADM00Handler.PresaveUser(objDTOUSR01);
                response = _objBLADM00Handler.Save();
            }
            return Ok(response);
        }

        /// <summary>
        /// Retrieves all users.
        /// </summary>
        /// <returns>HTTP response containing the retrieved users.</returns>
        [HttpGet]
        [Route("")]
        [AccessTokenAuthentication]
        [BasicAuthorization(Roles = "A")]
        public IHttpActionResult GetAdmin()
        {
            Response response = _objBLADM00Handler.RetrieveAdmin();
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
            Response response = _objBLADM00Handler.RetrieveAdmin(userID);
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
            Response response = _objBLADM00Handler.ValidateDelete(userID);
            if (!response.IsError)
            {
                response = _objBLADM00Handler.Delete(userID);
            }
            return Ok(response);
        }
    }
}