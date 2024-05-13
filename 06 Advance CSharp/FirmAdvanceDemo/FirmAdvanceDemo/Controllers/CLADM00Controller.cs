using FirmAdvanceDemo.Auth;
using FirmAdvanceDemo.BL;
using FirmAdvanceDemo.Enums;
using FirmAdvanceDemo.Models.DTO;
using FirmAdvanceDemo.Utility;
using System.Web.Http;

namespace FirmAdvanceDemo.Controllers
{
    /// <summary>
    /// Controller for managing administrative users.
    /// </summary>
    [RoutePrefix("api/admin")]
    public class CLADM00Controller : ApiController
    {
        #region Private Fields
        /// <summary>
        /// Handler for Admin
        /// </summary>
        private readonly BLADM00Handler _objBLADM00Handler;
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the CLADM00Controller class.
        /// </summary>
        public CLADM00Controller()
        {
            _objBLADM00Handler = new BLADM00Handler();
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Adds a new administrative user.
        /// </summary>
        /// <param name="objDTOUSR01">The DTO containing the administrative user data.</param>
        /// <returns>An IHttpActionResult indicating the result of the operation.</returns>
        [HttpPost]
        [Route("")]
        [AccessTokenAuthentication]
        [BasicAuthorization(Roles = "A")]
        public IHttpActionResult PostAdmin(DTOUSR01 objDTOUSR01)
        {
            Response response;
            _objBLADM00Handler.Operation = EnmOperation.A;
            response = _objBLADM00Handler.PrevalidateAdmin(objDTOUSR01);
            if (!response.IsError)
            {
                _objBLADM00Handler.PresaveAdmin(objDTOUSR01);
                response = _objBLADM00Handler.Save();
            }
            return Ok(response);
        }

        /// <summary>
        /// Updates an existing administrative user.
        /// </summary>
        /// <param name="objDTOUSR01">The DTO containing the updated administrative user data.</param>
        /// <returns>An IHttpActionResult indicating the result of the operation.</returns>
        [HttpPut]
        [Route("")]
        [AccessTokenAuthentication]
        [BasicAuthorization(Roles = "A")]
        public IHttpActionResult PutAdmin(DTOUSR01 objDTOUSR01)
        {
            Response response;
            _objBLADM00Handler.Operation = EnmOperation.E;
            response = _objBLADM00Handler.PrevalidateAdmin(objDTOUSR01);
            if (!response.IsError)
            {
                _objBLADM00Handler.PresaveAdmin(objDTOUSR01);
                response = _objBLADM00Handler.Save();
            }
            return Ok(response);
        }

        /// <summary>
        /// Retrieves all administrative users.
        /// </summary>
        /// <returns>An IHttpActionResult containing the retrieved administrative user data.</returns>
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
        /// Retrieves an administrative user by ID.
        /// </summary>
        /// <param name="userID">The ID of the administrative user to retrieve.</param>
        /// <returns>An IHttpActionResult containing the retrieved administrative user data.</returns>
        [HttpGet]
        [Route("{userID}")]
        [AccessTokenAuthentication]
        [BasicAuthorization(Roles = "A")]
        public IHttpActionResult GetAdmin(int userID)
        {
            Response response = _objBLADM00Handler.RetrieveAdmin(userID);
            return Ok(response);
        }

        /// <summary>
        /// Deletes an administrative user by ID.
        /// </summary>
        /// <param name="userID">The ID of the administrative user to delete.</param>
        /// <returns>An IHttpActionResult indicating the result of the operation.</returns>
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
        #endregion
    }
}