using FirmAdvanceDemo.BL;
using FirmAdvanceDemo.Utitlity;
using System.Web.Http;

namespace FirmAdvanceDemo.Controllers
{
    [RoutePrefix("api/salary")]
    public class CLSalaryController : BaseController
    {
        /// <summary>
        /// Instance of BLSalary
        /// </summary>
        private readonly BLSalary _objBLSalary;

        /// <summary>
        /// Default constructor for CLSalaryController
        /// </summary>
        public CLSalaryController()
        {
            _objBLSalary = new BLSalary();
        }

        [HttpPost]
        [Route("")]
        public IHttpActionResult PostSalary()
        {
            ResponseStatusInfo responseStatusInfo = _objBLSalary.CreditSalary();
            return Returner(responseStatusInfo);
        }
    }
}
