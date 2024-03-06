using FirmWebApiDemo.Authentication;
using FirmWebApiDemo.BL;
using FirmWebApiDemo.Utility;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Http;

namespace FirmWebApiDemo.Controllers
{
    /// <summary>
    /// Authentication Controller
    /// </summary>
    [RoutePrefix("api/token")]
    public class CLAuthenticationController : ApiController
    {
        /// <summary>
        /// Action method to generate an accesss token
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("generate")]
        [BasicAuthentication]
        public IHttpActionResult GetToken()
        {
            ClaimsIdentity identity = HttpContext.Current.User.Identity as ClaimsIdentity;

            int userId = int.Parse(identity.Claims.Where(claim => claim.Type == "r01f01")
                .Select(claim => claim.Value).SingleOrDefault());

            BLUser blUser = new BLUser();
            ResponseStatusInfo rsi = blUser.GetUsername(userId);

            if (!rsi.IsRequestSuccessful)
            {
                return BadRequest(rsi.Message);
            }

            string username = rsi.Data.ToString();
            string jwt = ValidateUser.GenerateJwt(userId, username);
            return Ok(ResponseWrapper.Wrap("access-token", jwt));
        }

        /// <summary>
        /// Action method to validate an access token
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("validate")]
        [BearerAuthentication]
        public IHttpActionResult GetData()
        {
            return Ok("Hello prajval, jwt");
        }
    }
}