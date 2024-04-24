using FirmAdvanceDemo.Auth;
using FirmAdvanceDemo.BL;
using FirmAdvanceDemo.Enums;
using FirmAdvanceDemo.Models.DTO;
using FirmAdvanceDemo.Utility;
using System.Web.Http;

namespace FirmAdvanceDemo.Controllers
{
    /// <summary>
    /// Controller for managing position operations, such as retrieving, adding, updating, and deleting positions.
    /// </summary>
    [RoutePrefix("api/position")]
    public class CLPSN01Controller : ApiController
    {
        /// <summary>
        /// Instance of the position handler for managing position operations.
        /// </summary>
        private readonly BLPSN01Handler _objBLPSN01Handler;

        /// <summary>
        /// Initializes a new instance of the CLPSN01Controller class.
        /// </summary>
        public CLPSN01Controller()
        {
            _objBLPSN01Handler = new BLPSN01Handler();
        }

        /// <summary>
        /// Retrieves all positions.
        /// </summary>
        /// <returns>HTTP response containing the retrieved positions.</returns>
        [HttpGet]
        [Route("")]
        [AccessTokenAuthentication]
        [BasicAuthorization(Roles = "A")]
        public IHttpActionResult GetPosition()
        {
            Response response = _objBLPSN01Handler.RetrievePosition();
            return Ok(response);
        }

        /// <summary>
        /// Retrieves a specific position by its ID.
        /// </summary>
        /// <param name="id">The ID of the position to retrieve.</param>
        /// <returns>HTTP response containing the retrieved position.</returns>
        [HttpGet]
        [Route("{id}")]
        [AccessTokenAuthentication]
        [BasicAuthorization(Roles = "A,E")]
        public IHttpActionResult GetPosition(int id)
        {
            Response response = _objBLPSN01Handler.RetrievePosition(id);
            return Ok(response);
        }

        /// <summary>
        /// Adds a new position.
        /// </summary>
        /// <param name="objDTOPSN01">DTO containing the details of the position to add.</param>
        /// <returns>HTTP response indicating the success or failure of the operation.</returns>
        [HttpPost]
        [Route("")]
        [AccessTokenAuthentication]
        [BasicAuthorization(Roles = "A")]
        public IHttpActionResult PostPosition(DTOPSN01 objDTOPSN01)
        {
            Response response;

            _objBLPSN01Handler.Operation = EnmOperation.A;

            response = _objBLPSN01Handler.Prevalidate(objDTOPSN01);

            if (!response.IsError)
            {
                _objBLPSN01Handler.Presave(objDTOPSN01);
                response = _objBLPSN01Handler.Validate();
                if (!response.IsError)
                {
                    response = _objBLPSN01Handler.Save();
                }
            }
            return Ok(response);
        }

        /// <summary>
        /// Updates an existing position.
        /// </summary>
        /// <param name="objDTOPSN01">DTO containing the updated details of the position.</param>
        /// <returns>HTTP response indicating the success or failure of the operation.</returns>
        [HttpPut]
        [Route("")]
        [AccessTokenAuthentication]
        [BasicAuthorization(Roles = "A")]
        public IHttpActionResult PutPosition(DTOPSN01 objDTOPSN01)
        {
            Response response;

            _objBLPSN01Handler.Operation = EnmOperation.E;

            response = _objBLPSN01Handler.Prevalidate(objDTOPSN01);

            if (!response.IsError)
            {
                _objBLPSN01Handler.Presave(objDTOPSN01);
                response = _objBLPSN01Handler.Validate();
                if (!response.IsError)
                {
                    response = _objBLPSN01Handler.Save();
                }
            }
            return Ok(response);
        }

        /// <summary>
        /// Deletes a position by its ID.
        /// </summary>
        /// <param name="positionId">The ID of the position to delete.</param>
        /// <returns>HTTP response indicating the success or failure of the operation.</returns>
        [HttpDelete]
        [Route("{positionId}")]
        [AccessTokenAuthentication]
        [BasicAuthorization(Roles = "A")]
        public IHttpActionResult DeletePosition(int positionId)
        {
            Response response = _objBLPSN01Handler.ValidateDelete(positionId);
            if (!response.IsError)
            {
                response = _objBLPSN01Handler.Delete(positionId);
            }
            return Ok(response);
        }
    }
}
