using FirmAdvanceDemo.BL;
using FirmAdvanceDemo.Utitlity;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.Http.Controllers;

namespace FirmAdvanceDemo.Auth
{
    /// <summary>
    /// Attribute to authenticate an accesss token
    /// </summary>
    public class AccessTokenAuthenticationAttribute : BearerAuthenticationAttribute
    {
        /// <summary>
        /// Method to perform authentication on access-token
        /// </summary>
        /// <param name="actionContext">context of current action</param>
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            string ErrorMessage = null;
            if (actionContext.Request.Headers.Authorization.Scheme != "Bearer")
            {
                ErrorMessage = "Invalid authentication request";
            }
            else
            {
                string jwt = actionContext.Request.Headers.Authorization.Parameter;
                ErrorMessage = this.AuthenticateJWT(jwt);
            }

            if (ErrorMessage != null)
            {
                actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized, ErrorMessage);
            }
        }
    }
}