using FirmAdvanceDemo.BL;
using FirmAdvanceDemo.Models.POCO;
using FirmAdvanceDemo.Utitlity;
using Newtonsoft.Json.Linq;
using System.Web.Http;

namespace FirmAdvanceDemo.Controllers
{
    [RoutePrefix("api/position")]
    public class CLPSN01Controller : BaseController
    {
        /// <summary>
        /// Instance of BLPosition
        /// </summary>
        private readonly BLPSN01Handler _objBLPosition;

        /// <summary>
        /// Default constructor for CLPositionController
        /// </summary>
        public CLPSN01Controller()
        {
            _objBLPosition = new BLPSN01Handler();
        }

        [HttpGet]
        [Route("")]
        public IHttpActionResult GetPositions()
        {
            Response




                = _objBLPosition.RetrieveResource();


        }

        [HttpGet]
        [Route("{id}")]
        public IHttpActionResult GetPosition(int id)
        {
            Response response = _objBLPosition.FetchResource(id);
            return Ok(response);
        }

        [HttpPost]
        [Route("")]
        public IHttpActionResult PostPosition(PSN01 Position)
        {
            Response response = _objBLPosition.AddResource(Position);
            return Ok(response);
        }

        [HttpPatch]
        [Route("{id}")]
        public IHttpActionResult PatchPosition(int id, JObject toUpdateJson)
        {
            Response response = _objBLPosition.UpdateResource(id, toUpdateJson);
            return Ok(response);
        }

        [HttpDelete]
        [Route("{id}")]
        public IHttpActionResult DeletePosition(int id)
        {
            Response response = _objBLPosition.RemoveResource(id);
            return Ok(response);
        }
    }
}
