using FirmAdvanceDemo.BL;
using FirmAdvanceDemo.Enums;
using FirmAdvanceDemo.Models.DTO;
using System.Web.Http;

namespace FirmAdvanceDemo.Controllers
{
    [RoutePrefix("api/employee")]
    public class CLEmployeeController : BaseController
    {
        /// <summary>
        /// Instance of BLEmployee
        /// </summary>
        private readonly BLEmployee _objBLEmployee;

        /// <summary>
        /// Default constructor for CLEmployeeController
        /// </summary>
        public CLEmployeeController() : base()
        {
            _objBLEmployee = new BLEmployee(_statusInfo);
        }

        [HttpPost]
        [Route("")]
        public IHttpActionResult Post(DTOUMP objDTOUMP)
        {
            if(_objBLEmployee.Prevalidate(objDTOUMP, EnmDBOperation.Create))
            {
                _objBLEmployee.Presave(objDTOUMP, EnmDBOperation.Create);
                if (_objBLEmployee.Validate())
                {
                    _objBLEmployee.Save(EnmDBOperation.Create);
                }
            }
            return Returner();
        }

        [HttpPatch]
        [Route("")]
        public IHttpActionResult Patch(DTOUMP objDTOUMP)
        {
            if (_objBLEmployee.Prevalidate(objDTOUMP, EnmDBOperation.Update))
            {
                _objBLEmployee.Presave(objDTOUMP, EnmDBOperation.Update);
                if (_objBLEmployee.Validate())
                {
                    _objBLEmployee.Save(EnmDBOperation.Update);
                }
            }
            return Returner();
        }
    }
}
