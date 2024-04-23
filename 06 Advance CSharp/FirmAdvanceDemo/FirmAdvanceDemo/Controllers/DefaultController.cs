using FirmAdvanceDemo.Auth;
using FirmAdvanceDemo.Enums;
using FirmAdvanceDemo.Models.POCO;
using ServiceStack.Data;
using ServiceStack.OrmLite;
using System.Data;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Http;

namespace FirmAdvanceDemo.Controllers
{
    public class DefaultController : ApiController
    {
        private IDbConnectionFactory _dfFactory;
        public DefaultController()
        {
            _dfFactory = OrmliteDbConnector.DbFactory;
        }

        [HttpGet]
        [Route("api/data/getdata")]
        public IHttpActionResult GetAppInfo()
        {
            return Ok("Welcome to FirmAdvanceDemo");
        }

        [HttpGet]
        [AccessTokenAuthentication]
        [Route("api/data/getUsername")]
        public object GetUsername()
        {
            ClaimsIdentity claimsIdentity = (ClaimsIdentity)HttpContext.Current.User.Identity;

            int userID = int.Parse(claimsIdentity?.Claims.FirstOrDefault(c => c.Type == "userID").Value);
            string username = "No user found.";
            using (IDbConnection db = _dfFactory.OpenDbConnection())
            {
                username = db.Scalar<USR01, string>(user => user.R01F02, user => user.R01F01 == userID);
            }

            return new { username = username };
        }

        [HttpGet]
        [AccessTokenAuthentication]
        [Route("api/data/getroles")]
        public object GetRoles()
        {
            ClaimsIdentity claimsIdentity = (ClaimsIdentity)HttpContext.Current.User.Identity;

            int userID = int.Parse(claimsIdentity?.Claims.FirstOrDefault(c => c.Type == "userID").Value);
            string roles = "No roles associated.";
            using (IDbConnection db = _dfFactory.OpenDbConnection())
            {
                SqlExpression<ULE02> sqlExp = db.From<ULE02>()
                    .Where(userRole => userRole.E02F02 == userID)
                    .Join<RLE01>((ur, r) => ur.E02F03 == r.E01F01)
                    .Select<RLE01>(r => r.E01F02);

                roles = db.Select<EnmRole>(sqlExp).Aggregate("", (acc, x) => acc + ", " + x);
            }

            return new { roles = roles };
        }
    }
}
