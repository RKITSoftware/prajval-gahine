using System.Web.Http;

namespace FirmAdvanceDemo.Controllers
{
    public class DefaultController
    {

        [HttpGet]
        [Route("api/data/getdata")]
        public IHttpActionResult GetAppInfo()
        {
            return Ok("Welcome to FirmAdvanceDemo");
        }
    }
}
