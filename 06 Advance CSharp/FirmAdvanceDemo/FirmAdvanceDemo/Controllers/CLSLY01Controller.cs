using FirmAdvanceDemo.BL;
using FirmAdvanceDemo.Utitlity;
using System.Web.Http;

namespace FirmAdvanceDemo.Controllers
{
    [RoutePrefix("api/salary")]
    public class CLSLY01Controller
    {
        /// <summary>
        /// Instance of BLSalary
        /// </summary>
        private readonly BLSLY01Handler _objBLSLY01Handler;

        /// <summary>
        /// Default constructor for CLSalaryController
        /// </summary>
        public CLSLY01Controller()
        {
            _objBLSLY01Handler = new BLSLY01Handler();
        }

        [HttpPost]
        [Route("credit")]
        public IHttpActionResult CreditSalary()
        {
            Response response = _objBLSLY01Handler.CreditSalary();
            return response;
        }
    }
}
