using FirmAdvanceDemo.Auth;
using FirmAdvanceDemo.BL;
using FirmAdvanceDemo.Utility;
using System.Web.Http;

namespace FirmAdvanceDemo.Controllers
{
    /// <summary>
    /// Controller for managing salary operations, such as crediting salaries.
    /// </summary>
    [RoutePrefix("api/salary")]
    public class CLSLY01Controller : ApiController
    {
        /// <summary>
        /// Instance of the salary handler for managing salary operations.
        /// </summary>
        private readonly BLSLY01Handler _objBLSLY01Handler;

        /// <summary>
        /// Initializes a new instance of the CLSLY01Controller class.
        /// </summary>
        public CLSLY01Controller()
        {
            _objBLSLY01Handler = new BLSLY01Handler();
        }

        /// <summary>
        /// Credits salary to employees.
        /// </summary>
        /// <returns>HTTP response indicating the success or failure of the operation.</returns>
        [HttpPost]
        [Route("credit")]
        [AccessTokenAuthentication]
        [BasicAuthorization(Roles = "A")]
        public IHttpActionResult CreditSalary()
        {
            Response response = _objBLSLY01Handler.CreditSalary();
            return Ok(response);
        }
    }
}
