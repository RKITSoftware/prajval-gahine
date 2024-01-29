using JwtAuthProject.Models;
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

namespace JwtAuthProject.Authentication
{
    public class BasicAuthenticationAttribute : AuthorizationFilterAttribute
    {
        /// <summary>
        /// calls when a process requests authorization
        /// </summary>
        /// <param name="actionContext">HttpActionContext</param>
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            // check if the request has an authorization header
            if (actionContext.Request.Headers.Authorization == null)
            {
                actionContext.Response = actionContext.Request.CreateErrorResponse(HttpStatusCode.Unauthorized, "Login failed");
            }
            else
            {
                // authorization header is present => so get the authToken
                string authTokenEncoded = actionContext.Request.Headers.Authorization.Parameter;

                // the authToken is base 64 encoded by default => so we need to decode it
                string authTokenDecoded = Encoding.UTF8.GetString(Convert.FromBase64String(authTokenEncoded));

                string[] username_password = authTokenDecoded.Split(':');

                string username = username_password[0];
                string password = username_password[1];

                if (ValidateUser.Login(username, password))
                {
                    // create new generic principal
                    // Thread.CurrentPrincipal = new GenericPrincipal(new GenericIdentity(username), null);

                    // get the user details
                    USR01 userDetails = ValidateUser.GetUserDetails(username, password);

                    // create an identity => i.e., attach username which is used to identify the user
                    GenericIdentity identity = new GenericIdentity(username);
                    // add claims for the identity => a claim has (claim_type, value)
                    identity.AddClaim(new Claim("r01f01", Convert.ToString(userDetails.r01f01)));

                    // create a principal that represent a user => it has an (identity object + roles)
                    IPrincipal principal = new GenericPrincipal(identity, userDetails.r01f04.Split(','));

                    // now associate the user/principal with the thread
                    Thread.CurrentPrincipal = principal;
                    if (HttpContext.Current != null)
                    {
                        // HttpContext is responsible for rq and res.
                        HttpContext.Current.User = principal;
                    }
                    else
                    {
                        actionContext.Response = actionContext.Request.CreateErrorResponse(HttpStatusCode.Unauthorized, "Authorization denied");
                    }
                }
                else
                {
                    actionContext.Response = actionContext.Request.CreateErrorResponse(HttpStatusCode.Unauthorized, "Login failed");
                }
            }
        }
    }
}