using FirmAdvanceDemo.Utitlity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http.Controllers;

namespace FirmAdvanceDemo.Auth
{
    /// <summary>
    /// Attribute to authenticate a refresh token
    /// </summary>
    public class RefreshTokenAuthenticationAttribute : BearerAuthenticationAttribute
    {
        /// <summary>
        /// Method to perform authentication on refresh-token
        /// </summary>
        /// <param name="actionContext">context of current action</param>
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            // access encrypted refresh token from cookie and decrypt it
            CookieHeaderValue cookie = actionContext.Request.Headers.GetCookies("refresh-token").FirstOrDefault<CookieHeaderValue>();
            string encryptedRefreshToken = cookie["refresh-token"].Value;
            string refreshToken = GeneralUtility.AesDecrypt(encryptedRefreshToken, null);

            string ErrorMessage = this.AuthenticateJWT(refreshToken);

            if (ErrorMessage != null)
            {
                actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized, ErrorMessage);
            }
        }
    }
}