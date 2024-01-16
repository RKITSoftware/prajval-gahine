using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Filters;
using System.Web.Http.Controllers;
using System.Net.Http;
using System.Net;
using System.Text;
using FirmWebApiDemo.Models;
using System.Security.Principal;
using System.Security.Claims;
using System.Threading;

namespace FirmWebApiDemo.Authentication
{
    public class BasicAuthentication : AuthorizationFilterAttribute
    {
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            // check if Authorization header is present
            if(actionContext.Request.Headers.Authorization == null)
            {
                actionContext.Response = actionContext.Request.CreateErrorResponse(HttpStatusCode.Unauthorized, "No Authorized token was found");
            }
            else
            {
                // authorization header is present
                // get the encoded token
                string encodedToken = actionContext.Request.Headers.Authorization.Parameter;

                // decode encoded token
                string decodedToken = Encoding.UTF8.GetString(Convert.FromBase64String(encodedToken));

                // get username and password in string arrray
                string[] username_password = decodedToken.Split(':');

                string username = username_password[0];
                string password = username_password[1];

                if(ValidateUser.Login(username, password))
                {
                    // get user details
                    USR01 existingUser = USR01.GetUsers().FirstOrDefault(user => user.r01f02 == username);

                    // create identity
                    GenericIdentity identity = new GenericIdentity(username);

                    // add claims to identity
                    identity.AddClaim(new Claim("r01f01", Convert.ToString(existingUser.r01f01)));

                    // create principal
                    IPrincipal principal = new GenericPrincipal(identity, existingUser.r01f04.Split(','));
                
                    // associate Thread with the principal and also with HttpContext
                    Thread.CurrentPrincipal = principal;
                    if(HttpContext.Current != null)
                    {
                        HttpContext.Current.User = principal;
                    }
                    else
                    {
                        actionContext.Response = actionContext.Request.CreateErrorResponse(HttpStatusCode.Unauthorized, "No HttpContext was associated with the request");
                    }
                }
                else
                {
                    actionContext.Response = actionContext.Request.CreateErrorResponse(HttpStatusCode.Unauthorized, "Invalid Credentials");
                }


            }
        }
    }
}