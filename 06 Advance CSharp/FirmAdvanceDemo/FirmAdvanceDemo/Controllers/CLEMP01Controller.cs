using FirmAdvanceDemo.BL;
using FirmAdvanceDemo.Enums;
using FirmAdvanceDemo.Models.DTO;
using System.Web.Http;

namespace FirmAdvanceDemo.Controllers
{
    /// <summary>
    /// Controller for managing employee operations.
    /// </summary>
    [RoutePrefix("api/employee")]
    public class CLEMP01Controller : BaseController
    {
        /// <summary>
        /// Instance of BLEmployee
        /// </summary>
        private readonly BLEMP01Handler _objBLEmployee;

        /// <summary>
        /// Default constructor for CLEmployeeController
        /// </summary>
        public CLEMP01Controller() : base()
        {
            _objBLEmployee = new BLEMP01Handler(_statusInfo);
        }

        [HttpGet]
        [Route("")]
        public IHttpActionResult Get()
        {
            return Ok();
        }

        [HttpGet]
        [Route("{id}")]
        public IHttpActionResult Get(int id)
        {
            return Ok();
        }

        [HttpPost]
        [Route("")]
        public IHttpActionResult Post(DTOUMP objDTOUMP)
        {
            if(_objBLEmployee.Prevalidate(objDTOUMP, EnmOperation.A))
            {
                _objBLEmployee.Presave(objDTOUMP, EnmOperation.A);
                if (_objBLEmployee.Validate())
                {
                    _objBLEmployee.Save(EnmOperation.A);
                }
            }
            return Returner();
        }

        [HttpPatch]
        [Route("")]
        public IHttpActionResult Patch(DTOUMP objDTOUMP)
        {
            if (_objBLEmployee.Prevalidate(objDTOUMP, EnmOperation.E))
            {
                _objBLEmployee.Presave(objDTOUMP, EnmOperation.E);
                if (_objBLEmployee.Validate())
                {
                    _objBLEmployee.Save(EnmOperation.E);
                }
            }
            return Returner();
        }
    }
}
