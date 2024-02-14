using JwtAuthProject.BL;
using JwtAuthProject.Models;
using Newtonsoft.Json.Linq;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace JwtAuthProject.Authentication
{
    /// <summary>
    /// Custome Jwt class to manage jwt token for authentication
    /// </summary>
    public class MyJwt
    {
        /// <summary>
        /// Validates jwt if corrupted or expired return false else true
        /// </summary>
        /// <param name="jwt">jwt token (EncodedHeader.EncodedPayload.EncodedHash)</param>
        /// <param name="secretKey">Jwt secret key</param>
        /// <returns></returns>
        public static bool IsJwtValid(string jwt, string secretKey)
        {
            string[] jwtArray = jwt.Split('.');
            string jwtHeader = jwtArray[0];
            string jwtPayload = jwtArray[1];
            string jwtHash = jwtArray[2];

            string payload = jwtHeader + "." + jwtPayload;

            // calculate hmac-sha-256 for the header and payload
            var hash = new HMACSHA256(Encoding.UTF8.GetBytes(secretKey));
            byte[] digest = hash.ComputeHash(Encoding.UTF8.GetBytes(payload));

            string digestBase64 = Convert.ToBase64String(digest)
                .Replace('+', '-')
                .Replace('/', '_')
                .Replace("=", "");


            System.Diagnostics.Debug.WriteLine(jwtHash);
            System.Diagnostics.Debug.WriteLine(digestBase64);
            System.Diagnostics.Debug.WriteLine("");

            // if jwt is valid then check for expiry time
            if (jwtHash.Equals(digestBase64))
            {
                // if not expired then return true else false

                // get current time
                TimeSpan span = DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1, 0, 0, 0));
                long currTotalSecond = (long)span.TotalSeconds;

                // decode jwtPayload from Base64
                int mod = jwtPayload.Length % 4;
                int padBits = mod > 0 ? 4 - mod : 0;
                string paddedJwtPayload = jwtPayload + new string('=', padBits);

                byte[] encodedData = Convert.FromBase64String(paddedJwtPayload);
                string decodedData = Encoding.UTF8.GetString(encodedData);

                // deserialize the jwtPayload decoded string
                var jwtPayloadObj = JwtPayload.Deserialize(decodedData);

                // get exp (expiry) claim
                object expiryTotalSecond;
                jwtPayloadObj.TryGetValue("exp", out expiryTotalSecond);

                long exp = (long)expiryTotalSecond;

                // compare expiry and curr time
                if (exp >= currTotalSecond)
                {
                    return true;
                }
            }
            return false;
        }
    }
    /// <summary>
    /// Authenticate the reuqest with jwt (bearer) as authentication type
    /// </summary>
    public class BearerAuthentication : AuthorizationFilterAttribute
    {
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            string tokenValue = actionContext.Request.Headers.Authorization.Parameter;

            // check jwt validity
            string secretKey = "mykeyisthisbecausemykeyisthisbecause";
            bool isValid = MyJwt.IsJwtValid(tokenValue, secretKey);

            // if jwt is invalid (corrupted-jwt or jwt-expired) then return unauthorized
            if (!isValid)
            {
                actionContext.Response = actionContext.Request.CreateErrorResponse(HttpStatusCode.Unauthorized, "invalid jwt, generate jwt using credentials");
            }
            else
            {
                // get userid (r01f01) from jwt payload
                // get jwt payload
                string jwtEncodedPayload = tokenValue.Split('.')[1];

                // pad jwtEncodedPayload
                int mod = jwtEncodedPayload.Length % 4;
                int padBits = mod > 0 ? 4 - mod : 0;
                jwtEncodedPayload = jwtEncodedPayload + new string('=', padBits);

                // decode the jwt payload
                byte[] decodedPayloadBytes = Convert.FromBase64String(jwtEncodedPayload);

                string decodedPayload = Encoding.UTF8.GetString(decodedPayloadBytes);

                JObject json = JObject.Parse(decodedPayload);

                USR01 user = BLUser.GetUser(int.Parse(json["r01f01"].ToString()));


                // create an identity => i.e., attach username which is used to identify the user
                GenericIdentity identity = new GenericIdentity(user.r01f02);
                // add claims for the identity => a claim has (claim_type, value)
                identity.AddClaim(new Claim("r01f01", Convert.ToString(user.r01f01)));

                // create a principal that represent a user => it has an (identity object + roles)
                IPrincipal principal = new GenericPrincipal(identity, user.r01f04.Split(','));

                // now associate the user/principal with the thread
                Thread.CurrentPrincipal = principal;
                if (HttpContext.Current != null)
                {
                    // HttpContext is responsible for rq and res.
                    HttpContext.Current.User = principal;
                }
                else
                {
                    actionContext.Response = actionContext.Request.CreateErrorResponse(HttpStatusCode.Unauthorized, "Authorization denied");
                }
            }
            // else let user enjoy the service
        }
    }
}