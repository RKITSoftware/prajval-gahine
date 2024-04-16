using FirmAdvanceDemo.BL;
using FirmAdvanceDemo.Utitlity;
using System.Web.Http;

namespace FirmAdvanceDemo.Controllers
{
    [RoutePrefix("api/salary")]
    public class CLSLY01Controller : BaseController
    {
        /// <summary>
        /// Instance of BLSalary
        /// </summary>
        private readonly BLSLY01Handler _objBLSalary;

        /// <summary>
        /// Default constructor for CLSalaryController
        /// </summary>
        public CLSLY01Controller()
        {
            _objBLSalary = new BLSLY01Handler();
        }

        [HttpPost]
        [Route("")]
        public IHttpActionResult PostSalary()
        {
            Response = _objBLSalary.CreditSalary();
            

        }
    }
}
