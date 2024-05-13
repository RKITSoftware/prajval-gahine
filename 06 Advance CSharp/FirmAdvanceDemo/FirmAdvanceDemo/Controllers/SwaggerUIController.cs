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
using System.Web.Http.Description;

namespace FirmAdvanceDemo.Controllers
{
    /// <summary>
    /// Controller for handling SwaggerUI API endpoints and retrieving user-related information.
    /// </summary>
    [ApiExplorerSettings(IgnoreApi = true)]
    [RoutePrefix("api/swaggerui-support")]
    public class SwaggerUIController : ApiController
    {
        #region Private Fields
        /// <summary>
        /// Connection factory for Ormlite
        /// </summary>
        private readonly IDbConnectionFactory _dfFactory;
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the SwaggerUIController class.
        /// </summary>
        public SwaggerUIController()
        {
            _dfFactory = OrmliteDbConnector.DbFactory;
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Retrieves application information.
        /// </summary>
        /// <returns>HTTP response containing the application information.</returns>
        [HttpGet]
        [Route("appinfo")]
        public IHttpActionResult GetAppInfo()
        {
            return Ok("Welcome to FirmAdvanceDemo");
        }

        /// <summary>
        /// Retrieves the username of the currently authenticated user.
        /// </summary>
        /// <returns>Object containing the username of the authenticated user.</returns>
        [HttpGet]
        [AccessTokenAuthentication]
        [Route("username")]
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

        /// <summary>
        /// Retrieves the roles associated with the currently authenticated user.
        /// </summary>
        /// <returns>Object containing the roles associated with the authenticated user.</returns>
        [HttpGet]
        [AccessTokenAuthentication]
        [Route("roles")]
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
        #endregion
    }
}
