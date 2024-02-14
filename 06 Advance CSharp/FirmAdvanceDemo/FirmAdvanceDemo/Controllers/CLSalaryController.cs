using FirmAdvanceDemo.BL;
using FirmAdvanceDemo.Utitlity;
using System.Net.Http;
using System.Web.Http;

namespace FirmAdvanceDemo.Controllers
{
    [RoutePrefix("api/salary")]
    public class CLSalaryController : ApiController
    {

        [NonAction]
        public IHttpActionResult Returner(ResponseStatusInfo responseStatusInfo)
        {
            if (responseStatusInfo.IsRequestSuccessful)
            {
                return Ok(ResponseWrapper.Wrap(responseStatusInfo.Message, responseStatusInfo.Data));
            }
            return ResponseMessage(Request.CreateErrorResponse(responseStatusInfo.StatusCode, responseStatusInfo.Message));
        }


        [HttpPost]
        [Route("")]
        public IHttpActionResult PostSalary()
        {
            ResponseStatusInfo responseStatusInfo = BLSalary.CreditSalary();
            return Returner(responseStatusInfo);
        }
    }
}
