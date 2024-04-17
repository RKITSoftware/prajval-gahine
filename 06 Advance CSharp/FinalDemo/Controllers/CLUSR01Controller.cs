using FinalDemo.BL;
using FinalDemo.FIlters;
using FinalDemo.Models.DTO;
using FinalDemo.Utilities;
using System.Web.Http;

namespace FinalDemo.Controllers
{
    /// <summary>
    /// Controller for managing user-related API endpoints.
    /// </summary>
    [RoutePrefix("api/user")]
    public class CLUSR01Controller : ApiController
    {
        #region Private Fields

        /// <summary>
        /// The handler for business logic operations related to the USR01 entity.
        /// </summary>
        private readonly BLUSR01Handler _handler;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the CLUSR01Controller class.
        /// </summary>
        public CLUSR01Controller()
        {
            _handler = new BLUSR01Handler();
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Retrieves all users from the database.
        /// </summary>
        /// <returns>An IHttpActionResult representing the HTTP response containing the users.</returns>
        [HttpGet]
        [Route("")]
        public IHttpActionResult Get()
        {
            Response response = _handler.GetUSR01();
            return Ok(response);
        }

        /// <summary>
        /// Retrieves a specific user by their ID from the database.
        /// </summary>
        /// <param name="id">The ID of the user to retrieve.</param>
        /// <returns>An IHttpActionResult representing the HTTP response containing the user.</returns>
        [HttpGet]
        [Route("{id}")]
        public IHttpActionResult Get(int id)
        {
            Response response = _handler.GetUSR01(id);
            return Ok(response);
        }

        /// <summary>
        /// Creates a new user in the database.
        /// </summary>
        /// <param name="objDTOUSR01">The DTO representing the user to be created.</param>
        /// <returns>An IHttpActionResult representing the HTTP response containing the result of the operation.</returns>
        [HttpPost]
        [Route("")]
        [ValidateModel]
        public IHttpActionResult Post(DTOUSR01 objDTOUSR01)
        {
            var x = ModelState.IsValid;
            Response response;

            _handler.Operation = EnmOperation.A;

            _handler.Presave(objDTOUSR01);

            response = _handler.Validate();

            if (!response.IsError)
            {
                response = _handler.Save();
            }
            return Ok(response);
        }

        /// <summary>
        /// Updates an existing user in the database.
        /// </summary>
        /// <param name="objDTOUSR01">The DTO representing the user to be updated.</param>
        /// <returns>An IHttpActionResult representing the HTTP response containing the result of the operation.</returns>
        [HttpPut]
        [Route("")]
        [ValidateModel]
        public IHttpActionResult Put(DTOUSR01 objDTOUSR01)
        {
            Response response;

            _handler.Operation = EnmOperation.E;

            _handler.Presave(objDTOUSR01);

            response = _handler.Validate();

            if (!response.IsError)
            {
                response = _handler.Save();
            }

            return Ok(response);
        }

        /// <summary>
        /// Deletes a user from the database by their ID.
        /// </summary>
        /// <param name="id">The ID of the user to delete.</param>
        /// <returns>An IHttpActionResult representing the HTTP response containing the result of the operation.</returns>
        [HttpDelete]
        [Route("{id}")]
        public IHttpActionResult Delete(int id)
        {
            Response response = _handler.ValidateDelete(id);
            if (!response.IsError)
            {
                response = _handler.DeleteUSR01(id);
            }
            return Ok(response);
        }

        #endregion
    }
}
