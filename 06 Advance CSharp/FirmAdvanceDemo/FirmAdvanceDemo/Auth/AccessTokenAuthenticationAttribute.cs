using FirmAdvanceDemo.Utility;
using System.Net;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace FirmAdvanceDemo.Auth
{
    /// <summary>
    /// Attribute to authenticate an access token.
    /// </summary>
    public class AccessTokenAuthenticationAttribute : AuthorizationFilterAttribute
    {
        /// <summary>
        /// Performs authentication on an access token.
        /// </summary>
        /// <param name="actionContext">The context of the current action.</param>
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            Response response;
            if (actionContext.Request.Headers.Authorization?.Scheme != "Bearer")
            {
                response = new Response
                {
                    IsError = true,
                    HttpStatusCode = HttpStatusCode.Unauthorized,
                    Message = "No bearer authentication scheme."
                };
            }
            else
            {
                string jwt = actionContext.Request.Headers.Authorization.Parameter;
                response = GeneralUtility.AuthenticateJWT(jwt);
            }

            if (response.IsError)
            {
                actionContext.Response = actionContext.Request.CreateResponse(response.HttpStatusCode, response.Message);
            }
        }
    }
}