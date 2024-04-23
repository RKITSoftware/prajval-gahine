using FirmAdvanceDemo.Utitlity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace FirmAdvanceDemo.Auth
{
    /// <summary>
    /// Attribute to authenticate a refresh token
    /// </summary>
    public class RefreshTokenAuthenticationAttribute : AuthorizationFilterAttribute
    {
        /// <summary>
        /// Method to perform authentication on refresh-token
        /// </summary>
        /// <param name="actionContext">context of current action</param>
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            Response response;

            // access encrypted refresh token from cookie and decrypt it
            CookieHeaderValue cookie = actionContext.Request.Headers.GetCookies("refresh-token").FirstOrDefault<CookieHeaderValue>();
            string encryptedRefreshToken = cookie["refresh-token"].Value;

            if (string.IsNullOrEmpty(encryptedRefreshToken))
            {
                response = new Response
                {
                    IsError = true,
                    HttpStatusCode = HttpStatusCode.Unauthorized,
                    Message = "Refresh token not found in cookie."
                };
            }
            else
            {
                string refreshToken = GeneralUtility.AesDecrypt(encryptedRefreshToken, null);

                response = GeneralUtility.AuthenticateJWT(refreshToken);
            }

            if (response.IsError)
            {
                actionContext.Response = actionContext.Request.CreateResponse(response.HttpStatusCode, response.Message);
            }
        }
    }
}