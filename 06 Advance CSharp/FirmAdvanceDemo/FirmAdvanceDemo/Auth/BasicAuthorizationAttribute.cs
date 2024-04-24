using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;

namespace FirmAdvanceDemo.Auth
{
    /// <summary>
    /// Attribute to perform authorization for an API hit on which this attribute is used.
    /// </summary>
    public class BasicAuthorizationAttribute : AuthorizeAttribute
    {
        /// <summary>
        /// Handles the unauthorized request by either allowing it or returning a forbidden status code.
        /// </summary>
        /// <param name="actionContext">The HTTP action context.</param>
        protected override void HandleUnauthorizedRequest(HttpActionContext actionContext)
        {
            if (HttpContext.Current.User.Identity.IsAuthenticated)
            {
                base.HandleUnauthorizedRequest(actionContext);
            }
            else
            {
                // User role was forbidden.
                actionContext.Response = new HttpResponseMessage(HttpStatusCode.Forbidden);
            }
        }
    }
}
