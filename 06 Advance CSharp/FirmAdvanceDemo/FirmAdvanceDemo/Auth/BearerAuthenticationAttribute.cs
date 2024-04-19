using FirmAdvanceDemo.BL;
using FirmAdvanceDemo.Utitlity;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Net.PeerToPeer;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.Http.Filters;
using System.Web.Security;
using System.Net;
using ServiceStack;

namespace FirmAdvanceDemo.Auth
{
    /// <summary>
    /// Attribute to authenticate user based on bearer token
    /// </summary>
    public abstract class BearerAuthenticationAttribute : AuthorizationFilterAttribute
    {
        /// <summary>
        /// Method to Authenticate a jwt (hash and expiry)
        /// </summary>
        /// <param name="jwt">Json web token</param>
        /// <returns>return error string message if token is incorrect else return null</returns>
        public Response AuthenticateJWT(string jwt)
        {
            Response response = new Response();

            string[] headerEn_payloadEn_digest = jwt.Split('.');
            if (headerEn_payloadEn_digest.Length != 3)
            {
                response.IsError = true;
                response.HttpStatusCode = HttpStatusCode.Unauthorized;
                response.Message = "Invalid JWT: Token is malformed.";

                return response;
            }

            // get headerEn, payloadEn and digest
            string headerEn = headerEn_payloadEn_digest[0];
            string payloadEn = headerEn_payloadEn_digest[1];
            string digest = headerEn_payloadEn_digest[2];

            string digestToCompute = GeneralUtility.GetHMACBase64($"{headerEn}.{payloadEn}", null)
                .Replace('/', '_')
                .Replace('+', '-')
                .Replace("=", "");

            if (!digestToCompute.Equals(digest))
            {
                response.IsError = true;
                response.HttpStatusCode = HttpStatusCode.Unauthorized;
                response.Message = "Invalid JWT.";

                return response;
            }

            // then check the expiry of jwt
            // decode the payoload => get the expire property value
            // check with current time
            long currentTime = DateTimeOffset.UtcNow.ToUnixTimeSeconds();

            JObject payloadJson = JsonConvert.DeserializeObject<JObject>(Encoding.UTF8.GetString(Convert.FromBase64String(payloadEn)));
            long expires = long.Parse((string)payloadJson["expires"]);

            if(expires < currentTime)
            {
                response.IsError = true;
                response.HttpStatusCode = HttpStatusCode.Unauthorized;
                response.Message = "JWT expired.";

                return response;
            }

            // check for existance of user (by userId)
            // if exists => then get username and its roles
            int userId = (int)payloadJson["id"];

            bool userIdExists = GeneralHandler.CheckUserIDExists(userId);

            if(!userIdExists)
            {
                response.IsError = true;
                response.HttpStatusCode = HttpStatusCode.NotFound;
                response.Message = $"User {userId} not found.";

                return response;
            }

            string username = GeneralHandler.RetrieveUsernameByUserID(userId);
            string[] lstRole = GeneralContext.FetchRolesByUserID(userId);
            int employeeID = GeneralHandler.RetrieveEmployeeIDByUserID(userId);

            GenericIdentity identity = new GenericIdentity(username);
            identity.AddClaim(new Claim("userID", userId.ToString()));

            HttpContext.Current.Items["employeeID"] = employeeID;
            HttpContext.Current.Items["username"] = username;

            GenericPrincipal principal = new GenericPrincipal(identity, lstRole);

            Thread.CurrentPrincipal = principal;
            if (HttpContext.Current != null)
            {
                HttpContext.Current.User = principal;
            }

            return response;
        }

    }
}