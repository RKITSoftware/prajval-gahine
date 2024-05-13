using FirmAdvanceDemo.Auth;
using FirmAdvanceDemo.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Web.Http;

namespace FirmAdvanceDemo.Controllers
{
    /// <summary>
    /// Controller for handling authentication-related operations.
    /// </summary>
    public class CLAuthController : ApiController
    {
        #region Public Methods
        /// <summary>
        /// Retrieves a refresh token for the authenticated user.
        /// </summary>
        [HttpGet]
        [Route("api/getrefreshtoken")]
        [BasicAuthentication]
        public HttpResponseMessage GetRefreshToken()
        {
            IEnumerable<Claim> claims = ((ClaimsIdentity)User.Identity).Claims;

            // get user ID from identity
            int userId = int.Parse(claims.Where(c => c.Type == "userID")
                   .Select(c => c.Value).SingleOrDefault());

            string header = "{\"alg\":\"HS256\",\"typ\":\"JWT\"}";
            string payload = $"{{\"id\":\"{userId}\",\"expires\":\"{DateTimeOffset.UtcNow.ToUnixTimeSeconds() + 7776000}\"}}";

            string refreshToken = GeneralUtility.GenerateJWT(header, payload);

            // encrypt the refresh token
            string encryptedRefreshToken = GeneralUtility.AesEncrypt(refreshToken, null);

            // set the refresh token in cookie
            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, "refresh-token");
            CookieHeaderValue cookie = new CookieHeaderValue("refresh-token", encryptedRefreshToken)
            {
                HttpOnly = true
            };

            response.Headers.AddCookies(new CookieHeaderValue[] { cookie });
            response.StatusCode = HttpStatusCode.OK;

            return response;
        }

        /// <summary>
        /// Retrieves an access token for the authenticated user.
        /// </summary>
        [HttpGet]
        [Route("api/gettoken")]
        [RefreshTokenAuthentication]
        public string GetAccessToken()
        {
            IEnumerable<Claim> claims = ((ClaimsIdentity)User.Identity).Claims;

            // get user ID from identity
            int userId = int.Parse(claims.Where(c => c.Type == "userID")
                   .Select(c => c.Value).SingleOrDefault());

            string header = "{\"alg\":\"HS256\",\"typ\":\"JWT\"}";
            string payload = $"{{\"id\":\"{userId}\",\"expires\":\"{DateTimeOffset.UtcNow.ToUnixTimeSeconds() + 7890000}\"}}";

            string accessToken = GeneralUtility.GenerateJWT(header, payload);

            return accessToken;
        }
        #endregion
    }
}
