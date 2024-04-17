using FirmAdvanceDemo.Auth;
using FirmAdvanceDemo.BL;
using FirmAdvanceDemo.Enums;
using FirmAdvanceDemo.Models.DTO;
using FirmAdvanceDemo.Utitlity;
using System.Web.Http;

namespace FirmAdvanceDemo.Controllers
{
    [RoutePrefix("api/department")]
    public class CLDPT01Controller : ApiController
    {
        /// <summary>
        /// Instance of BLDepartment
        /// </summary>
        private readonly BLDPT01Handler _objBLDPT01Handler;

        /// <summary>
        /// Default constructor for CLDepartmentController
        /// </summary>
        public CLDPT01Controller()
        {
            _objBLDPT01Handler = new BLDPT01Handler();
        }


        [HttpGet]
        [Route("")]
        public IHttpActionResult GetDepartment()
        {
            Response response = _objBLDPT01Handler.RetrieveDepartment();
            return Ok(response);
        }

        [HttpGet]
        [Route("{departmentId}")]
        public IHttpActionResult GetDepartment(int departmentId)
        {
            Response response = _objBLDPT01Handler.RetrieveDepartment(departmentId);
            return Ok(response);
        }

        [HttpPost]
        [Route("")]
        [AccessTokenAuthentication]
        [BasicAuthorization(Roles = "admin")]
        public IHttpActionResult PostDepartment(DTODPT01 objDTODPT01)
        {
            Response response;

            _objBLDPT01Handler.Operation = EnmOperation.A;

            _objBLDPT01Handler.Presave(objDTODPT01);

            response = _objBLDPT01Handler.Valdiate();

            if (!response.IsError)
            {
                response = _objBLDPT01Handler.Save();
            }
            return Ok(response);
        }

        [HttpPut]
        [Route("")]
        public IHttpActionResult PutDepartment(DTODPT01 objDTODPT01)
        {
            Response response;

            _objBLDPT01Handler.Operation = EnmOperation.E;

            _objBLDPT01Handler.Presave(objDTODPT01);

            response = _objBLDPT01Handler.Valdiate();

            if (!response.IsError)
            {
                response = _objBLDPT01Handler.Save();
            }
            return Ok(response);
        }

        [HttpDelete]
        [Route("{departmentId}")]
        public IHttpActionResult DeleteDepartment(int departmentId)
        {
            Response response = _objBLDPT01Handler.ValidateDelete(departmentId);

            if (!response.IsError)
            {
                response = _objBLDPT01Handler.Delete(departmentId);
            }
            return Ok(response);
        }
    }
}
