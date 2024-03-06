using FirmAdvanceDemo.BL;
using FirmAdvanceDemo.Utitlity;
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
                        int EmlpoyeeId = BLEmployee.FetchEmployeeIdByUserId(userId);
                        bool IsPrincipalAttached = GeneralUtility.AttachPrinicpal(userId.ToString(), username, EmlpoyeeId.ToString(), roles);

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