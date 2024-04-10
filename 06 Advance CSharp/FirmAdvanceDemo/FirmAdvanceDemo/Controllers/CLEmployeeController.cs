using FirmAdvanceDemo.BL;
using FirmAdvanceDemo.Enums;
using FirmAdvanceDemo.Models.DTO;
using FirmAdvanceDemo.Utitlity;
using Microsoft.SqlServer.Server;
using System.Security.Cryptography;
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
        public IHttpActionResult Post(DTOUMP objUSREMP)
        {
            if(_objBLEmployee.Prevalidate(objUSREMP, EnmDBOperation.Create))
            {
                _objBLEmployee.Presave(objUSREMP, EnmDBOperation.Create);
                if (_objBLEmployee.Validate())
                {
                    _objBLEmployee.Save(EnmDBOperation.Create);
                }
            }
            return Returner();
        }
    }
}
