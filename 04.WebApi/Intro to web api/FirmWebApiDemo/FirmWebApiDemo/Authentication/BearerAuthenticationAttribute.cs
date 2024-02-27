using FirmWebApiDemo.BL;
using FirmWebApiDemo.Models;
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
using System.Web.Http.Filters;

namespace FirmWebApiDemo.Authentication
{
    public class BearerAuthenticationAttribute : AuthorizationFilterAttribute
    {
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            // get the jwt
            string scheme = actionContext.Request.Headers.Authorization.Scheme;
            if (scheme == "Bearer")
            {
                string jwt = actionContext.Request.Headers.Authorization.Parameter;
                bool isJwtValid = ValidateUser.ValidateJwt(jwt);
                if (isJwtValid)
                {
                    string EncodedPaylaod = jwt.Split('.')[1];
                    string payload = Encoding.UTF8.GetString(Convert.FromBase64String(EncodedPaylaod));
                    JObject jsonPayload = JObject.Parse(payload);

                    string username = jsonPayload["username"].ToString();
                    string userId = jsonPayload["userId"].ToString();

                    BLUser blUser = new BLUser();
                    USR01 user = blUser.GetUser(int.Parse(userId));

                    // create identity
                    GenericIdentity identity = new GenericIdentity(username, scheme);

                    // add claims to identity
                    identity.AddClaim(new Claim("r01f01", userId));

                    // create principal
                    IPrincipal principal = new GenericPrincipal(identity, user.r01f04.Split(','));

                    // associate Thread with the principal and also with HttpContext
                    Thread.CurrentPrincipal = principal;
                    if (HttpContext.Current != null)
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
                    actionContext.Response = actionContext.Request.CreateErrorResponse(HttpStatusCode.Unauthorized, "Invalid Jwt");
                }
            }
            else
            {
                // not a bearer scheme
                actionContext.Response = actionContext.Request.CreateErrorResponse(HttpStatusCode.Unauthorized, "Bad Authentcation Scheme");
            }
        }
    }
}