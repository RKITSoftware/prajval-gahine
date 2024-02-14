using System.Web.Http;
using CustomJwtAuth.Authentication;

namespace CustomJwtAuth.Controllers
{
    [BasicAuthentication]
    public class DataController : ApiController
    {
        [HttpGet]
        [Route("api/data/getdata")]
        [BasicAuthorizationAttribute(Roles = "admin")]
        public IHttpActionResult GetData()
        {
            return Ok(new { name = "prajavl", age = 25 });
        }
    }
}
