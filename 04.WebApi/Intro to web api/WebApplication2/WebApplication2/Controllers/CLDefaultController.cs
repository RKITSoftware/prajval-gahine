using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using WebApplication2.Authentication;

namespace WebApplication2.Controllers
{
    /// <summary>
    /// Default controller to check server is setup properly
    /// </summary>
    public class CLDefaultController : ApiController
    {
        [HttpGet]
        [Route("")]
        //[Authorize]
        //[BasicAuthenticationAttribute]
        public IHttpActionResult GetData()
        {
            
            return Ok("Welcome to web application, try api/{controller}/{id} to enjoy api service");    
        }
    }
}
