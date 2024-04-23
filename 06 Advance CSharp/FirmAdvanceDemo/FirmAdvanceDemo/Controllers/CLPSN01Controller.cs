using FirmAdvanceDemo.Auth;
using FirmAdvanceDemo.BL;
using FirmAdvanceDemo.Enums;
using FirmAdvanceDemo.Models.DTO;
using FirmAdvanceDemo.Utitlity;
using System.Web.Http;

namespace FirmAdvanceDemo.Controllers
{
    [RoutePrefix("api/position")]
    public class CLPSN01Controller : ApiController
    {
        /// <summary>
        /// Instance of BLPosition
        /// </summary>
        private readonly BLPSN01Handler _objBLPSN01Handler;

        /// <summary>
        /// Default constructor for CLPositionController
        /// </summary>
        public CLPSN01Controller()
        {
            _objBLPSN01Handler = new BLPSN01Handler();
        }

        [HttpGet]
        [Route("")]
        [AccessTokenAuthentication]
        [BasicAuthorization(Roles = "A")]
        public IHttpActionResult GetPosition()
        {
            Response response = _objBLPSN01Handler.RetrievePosition();
            return Ok(response);
        }

        [HttpGet]
        [Route("{id}")]
        [AccessTokenAuthentication]
        [BasicAuthorization(Roles = "A,E")]
        public IHttpActionResult GetPosition(int id)
        {
            Response response = _objBLPSN01Handler.RetrievePosition(id);
            return Ok(response);
        }

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
