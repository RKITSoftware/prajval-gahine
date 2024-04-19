using FirmAdvanceDemo.Auth;
using FirmAdvanceDemo.Utitlity;
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
    public class CLAuthController : ApiController
    {
        /// <summary>
        /// Non action method to generate a jwt based on header and payload
        /// </summary>
        /// <param name="header">Jwt Header</param>
        /// <param name="payload">Jwt Payload</param>
        /// <returns></returns>
        [NonAction]
        public string GenerateJWT(string header, string payload)
        {
            string headerEncoded = Convert.ToBase64String(Encoding.UTF8.GetBytes(header));
            string payloadEncoded = Convert.ToBase64String(Encoding.UTF8.GetBytes(payload));

            string digest = GeneralUtility.GetHMACBase64($"{headerEncoded}.{payloadEncoded}", null)
                .Replace('/', '_')
                .Replace('+', '-')
                .Replace("=", "");

            return $"{headerEncoded}.{payloadEncoded}.{digest}";
        }

        [HttpGet]
        [Route("api/getrefreshtoken")]
        [BasicAuthentication]
        public HttpResponseMessage GetRefreshToken()
        {
            IEnumerable<Claim> claims = ((ClaimsIdentity)User.Identity).Claims;

            // get user ID from identity
            int userId = int.Parse(claims.Where(c => c.Type == "Id")
                   .Select(c => c.Value).SingleOrDefault());

            string header = "{\"alg\":\"HS256\",\"typ\":\"JWT\"}";
            string payload = $"{{\"id\":\"{userId}\",\"expires\":\"{DateTimeOffset.UtcNow.ToUnixTimeSeconds() + 7776000}\"}}";

            string refreshToken = GenerateJWT(header, payload);

            // encrypt the refresh token
            string encryptedRefreshToken = GeneralUtility.AesEncrypt(refreshToken, null);

            // set the refreshtoken in cookie
            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, "refresh-token");
            CookieHeaderValue cookie = new CookieHeaderValue("refresh-token", encryptedRefreshToken)
            {
                HttpOnly = true
            };

            response.Headers.AddCookies(new CookieHeaderValue[] { cookie });
            response.StatusCode = HttpStatusCode.OK;

            return response;
        }


        [HttpGet]
        [Route("api/gettoken")]
        [RefreshTokenAuthentication]
        public string GetAccessToken()
        {
            IEnumerable<Claim> claims = ((ClaimsIdentity)User.Identity).Claims;

            // get user ID from identity
            int userId = int.Parse(claims.Where(c => c.Type == "Id")
                   .Select(c => c.Value).SingleOrDefault());

            string header = "{\"alg\":\"HS256\",\"typ\":\"JWT\"}";
            string payload = $"{{\"id\":\"{userId}\",\"expires\":\"{DateTimeOffset.UtcNow.ToUnixTimeSeconds() + 7890000}\"}}";

            string accessToken = GenerateJWT(header, payload);

            return accessToken;
        }
    }
}
