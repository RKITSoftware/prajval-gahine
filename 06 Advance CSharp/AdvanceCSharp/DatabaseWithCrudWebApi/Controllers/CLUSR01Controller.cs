using DatabaseWithCrudWebApi.BL;
using DatabaseWithCrudWebApi.Models;
using System.Web.Http;
using System.Data;
using System.Net;

namespace DatabaseWithCrudWebApi.Controllers
{
    /// <summary>
    /// Controller for handling user-related API requests.
    /// </summary>
    [RoutePrefix("api/user")]
    public class CLUSR01Controller : ApiController
    {
        #region Public Fields
        
        private readonly BLUSR01Handler _objBLUser;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the CLUSR01Controller class.
        /// </summary>
        public CLUSR01Controller()
        {
            _objBLUser = new BLUSR01Handler();
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Retrieves a list of all users.
        /// </summary>
        /// <returns>An IHttpActionResult containing the list of users.</returns>
        [Route("")]
        [HttpGet]
        public IHttpActionResult Get()
        {
            Response response = new Response();
            
        }

        [Route("{id}")]
        public IHttpActionResult GetById(int id)
        {
            return Ok(_objBLUser.GetUSR01(id));
        }

        /// <summary>
        /// Creates a new user.
        /// </summary>
        /// <param name="objDTOUSR01">The data transfer object representing the new user.</param>
        /// <returns>An IHttpActionResult indicating the result of the operation.</returns>
        [Route("")]
        [HttpPost]
        public IHttpActionResult Post(DTOUSR01 objDTOUSR01)
        {
            _objBLUser.Operation = EnmOperation.Create;

            _objBLUser.Presave(objDTOUSR01);
            //ResponseInfo response = _objBLUser.Validate() ?? _objBLUser.Save();
            Response response = _objBLUser.Validate();
            if(response != null)
            {
                response = _objBLUser.Save();
            }
            return Ok(response);
        }

        /// <summary>
        /// Updates an existing user.
        /// </summary>
        /// <param name="objDTOUSR01">The data transfer object representing the updated user.</param>
        /// <returns>An IHttpActionResult indicating the result of the operation.</returns>
        [Route("")]
        [HttpPatch]
        public IHttpActionResult Patch(DTOUSR01 objDTOUSR01)
        {
            _objBLUser.Operation = EnmOperation.Update;

            _objBLUser.Presave(objDTOUSR01);

            Response response = _objBLUser.Validate() ?? _objBLUser.Save();
            return Ok(response);
        }

        /// <summary>
        /// Deletes a user with the specified user ID.
        /// </summary>
        /// <param name="userId">The ID of the user to delete.</param>
        /// <returns>An IHttpActionResult indicating the result of the operation.</returns>
        [Route("{id}")]
        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            Response response = _objBLUser.ValidateDelete(id) ?? _objBLUser.Delete(id);
            return Ok(response);
        }

        #endregion
    }
}
