using FirmAdvanceDemo.BL;
using FirmAdvanceDemo.Models.POCO;
using FirmAdvanceDemo.Utitlity;
using Newtonsoft.Json.Linq;
using System.Web.Http;

namespace FirmAdvanceDemo.Controllers
{
    [RoutePrefix("api/position")]
    public class CLPositionController : BaseController
    {
        /// <summary>
        /// Instance of BLPosition
        /// </summary>
        private readonly BLPosition _objBLPosition;

        /// <summary>
        /// Default constructor for CLPositionController
        /// </summary>
        public CLPositionController()
        {
            _objBLPosition = new BLPosition();
        }

        [HttpGet]
        [Route("")]
        public IHttpActionResult GetPositions()
        {
            ResponseStatusInfo responseStatusInfo = _objBLPosition.FetchResource();
            return Returner(responseStatusInfo);
        }

        [HttpGet]
        [Route("{id}")]
        public IHttpActionResult GetPosition(int id)
        {
            ResponseStatusInfo responseStatusInfo = _objBLPosition.FetchResource(id);
            return Returner(responseStatusInfo);
        }

        [HttpPost]
        [Route("")]
        public IHttpActionResult PostPosition(PSN01 Position)
        {
            ResponseStatusInfo responseStatusInfo = _objBLPosition.AddResource(Position);
            return Returner(responseStatusInfo);
        }

        [HttpPatch]
        [Route("{id}")]
        public IHttpActionResult PatchPosition(int id, JObject toUpdateJson)
        {
            ResponseStatusInfo responseStatusInfo = _objBLPosition.UpdateResource(id, toUpdateJson);
            return Returner(responseStatusInfo);
        }

        [HttpDelete]
        [Route("{id}")]
        public IHttpActionResult DeletePosition(int id)
        {
            ResponseStatusInfo responseStatusInfo = _objBLPosition.RemoveResource(id);
            return Returner(responseStatusInfo);
        }
    }
}
