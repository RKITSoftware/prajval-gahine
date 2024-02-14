using System;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using static ServiceStack.LicenseUtils;

namespace FirmAdvanceDemo.Auth
{
    /// <summary>
    /// Attribute to perform user authentication based on username ad password
    /// </summary>
    public class BasicAuthenticationAttribute : AuthorizationFilterAttribute
    {
        /// <summary>
        /// Method to create and attach principal to current thread and http context
        /// </summary>
        /// <param name="userId">user id</param>
        /// <param name="username">username</param>
        /// <param name="roles">user roles</param>
        /// <returns></returns>
        public bool AttachPrinicpal(string userId, string username, string[] roles)
        {
            GenericIdentity identity = new GenericIdentity(username);
            identity.AddClaim(new Claim("Id", userId));
            identity.AddClaim(new Claim("Username", username));

            GenericPrincipal principal = new GenericPrincipal(identity, roles);

            Thread.CurrentPrincipal = principal;
            if (HttpContext.Current != null)
            {
                HttpContext.Current.User = principal;
                return true;
            }
            return false;
        }

        public override void OnAuthorization(HttpActionContext actionContext)
        {
            if (actionContext?.Request?.Headers?.Authorization == null)
            {
                actionContext.Response = actionContext.Request.CreateErrorResponse(HttpStatusCode.Unauthorized, "Login failed");
            }
            else
            {
                if (actionContext?.Request?.Headers?.Authorization?.Scheme == "Basic")
                {
                    string token = actionContext.Request.Headers.Authorization.Parameter;
                    string tokenDecoded = Encoding.UTF8.GetString(Convert.FromBase64String(token));

                    string[] username_password = tokenDecoded.Split(':');

                    string username = username_password[0];
                    string password = username_password[1];

                    int userId = -1;
                    string[] roles;
                    bool IsUserValid = ValidateUser.Login(username, password, out userId, out roles);

                    if (IsUserValid)
                    {
                        bool IsPrincipalAttached = this.AttachPrinicpal(userId.ToString(), username, roles);

                        if(!IsPrincipalAttached)
                        {
                            actionContext.Response = actionContext.Request.CreateErrorResponse(HttpStatusCode.Unauthorized, "Authorization denied");
                        }
                    }
                    else
                    {
                        actionContext.Response = actionContext.Request.CreateErrorResponse(HttpStatusCode.Unauthorized, "Invalida Credentials");
                    }
                }
                else
                {
                    actionContext.Response = actionContext.Request.CreateErrorResponse(HttpStatusCode.Unauthorized, "Invalid authentication request");
                }
            }
        }
    }
}