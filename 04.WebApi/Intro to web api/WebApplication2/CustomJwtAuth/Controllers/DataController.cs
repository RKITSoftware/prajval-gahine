using System.Web.Http;

namespace CustomJwtAuth.Controllers
{
    public class DataController : ApiController
    {
        [HttpGet]
        [Route("api/data/getdata")]
        public IHttpActionResult GetData()
        {
            return Ok(new { name = "prajavl", age = 25 });
        }
    }
}
