using FirmAdvanceDemo.BL;
using FirmAdvanceDemo.Utitlity;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.Http.Controllers;
using static ServiceStack.LicenseUtils;
using System.Net;

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