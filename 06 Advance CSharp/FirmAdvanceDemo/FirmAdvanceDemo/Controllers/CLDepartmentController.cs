using FirmAdvanceDemo.BL;
using FirmAdvanceDemo.Models.POCO;
using FirmAdvanceDemo.Utitlity;
using Newtonsoft.Json.Linq;
using System.Web.Http;

namespace FirmAdvanceDemo.Controllers
{
    [RoutePrefix("api/department")]
    public class CLDepartmentController : BaseController
    {


        /// <summary>
        /// Instance of BLDepartment
        /// </summary>
        private readonly BLDepartment _objBLDepartment;

        /// <summary>
        /// Default constructor for CLDepartmentController
        /// </summary>
        public CLDepartmentController()
        {
            _objBLDepartment = new BLDepartment();
        }


        [HttpGet]
        [Route("")]
        public IHttpActionResult GetDepartments()
        {
            ResponseStatusInfo responseStatusInfo = _objBLDepartment.FetchResource();
            return Returner(responseStatusInfo);
        }

        [HttpGet]
        [Route("{id}")]
        public IHttpActionResult GetDepartment(int id)
        {
            ResponseStatusInfo responseStatusInfo = _objBLDepartment.FetchResource(id);
            return Returner(responseStatusInfo);
        }

        [HttpPost]
        [Route("")]
        public IHttpActionResult PostDepartment(DPT01 Department)
        {
            ResponseStatusInfo responseStatusInfo = _objBLDepartment.AddResource(Department);
            return Returner(responseStatusInfo);
        }

        [HttpPatch]
        [Route("{id}")]
        public IHttpActionResult PatchDepartment(int id, JObject toUpdateJson)
        {
            ResponseStatusInfo responseStatusInfo = _objBLDepartment.UpdateResource(id, toUpdateJson);
            return Returner(responseStatusInfo);
        }

        [HttpDelete]
        [Route("{id}")]
        public IHttpActionResult DeleteDepartment(int id)
        {
            ResponseStatusInfo responseStatusInfo = _objBLDepartment.RemoveResource(id);
            return Returner(responseStatusInfo);
        }
    }
}
