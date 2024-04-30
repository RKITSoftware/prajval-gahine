using FirmAdvanceDemo.Utility;
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

namespace FirmAdvanceDemo.Auth
{
    /// <summary>
    /// Attribute to perform user authentication based on username and password.
    /// </summary>
    public class BasicAuthenticationAttribute : AuthorizationFilterAttribute
    {
        /// <summary>
        /// Performs basic authentication based on the Authorization header.
        /// </summary>
        /// <param name="actionContext">The HTTP action context.</param>
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            Response response = new Response();

            if (actionContext?.Request?.Headers?.Authorization == null)
            {
                response.IsError = true;
                response.HttpStatusCode = HttpStatusCode.Unauthorized;
                response.Message = "No Authorization Header found, login failed.";
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

                    bool isValid = GeneralUtility.Login(username, password);

                    if (isValid)
                    {
                        // get userID
                        int userID = GeneralHandler.RetrieveUserIdByUsername(username);

                        int employeeID = GeneralHandler.RetrieveemployeeIDByUserID(userID);

                        GenericIdentity identity = new GenericIdentity(username);
                        identity.AddClaim(new Claim("userID", userID.ToString()));

                        HttpContext.Current.Items["employeeID"] = employeeID;
                        HttpContext.Current.Items["username"] = username;

                        string[] roles = GeneralContext.FetchRolesByUserID(userID);

                        GenericPrincipal principal = new GenericPrincipal(identity, roles);

                        Thread.CurrentPrincipal = principal;
                        if (HttpContext.Current != null)
                        {
                            HttpContext.Current.User = principal;
                        }
                    }
                    else
                    {
                        response.IsError = true;
                        response.HttpStatusCode = HttpStatusCode.Unauthorized;
                        response.Message = "Invalid credentials.";
                    }
                }
                else
                {
                    response.IsError = true;
                    response.HttpStatusCode = HttpStatusCode.Unauthorized;
                    response.Message = "Invalid authorization schema.";
                }
            }

            if (response.IsError)
            {
                actionContext.Response = actionContext.Request.CreateErrorResponse(response.HttpStatusCode, response.Message);
            }
        }
    }
}