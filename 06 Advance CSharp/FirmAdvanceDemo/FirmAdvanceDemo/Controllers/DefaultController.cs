using System.Web.Http;

namespace FirmAdvanceDemo.Controllers
{
    public class DefaultController : ApiController
    {
        [HttpGet]
        public IHttpActionResult GetAppInfo()
        {
            return Ok("Welcome to FirmAdvanceDemo");
        }
    }
}
