using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;

namespace FirmWebApiDemo.Authentication
{
    /// <summary>
    /// Attribute to handle authorizatoion of request
    /// </summary>
    public class BasicAuthorization : AuthorizeAttribute
    {
        /// <summary>
        /// Mehtod to handle unauthorized request
        /// </summary>
        /// <param name="actionContext">Action Context</param>
        protected override void HandleUnauthorizedRequest(HttpActionContext actionContext)
        {
            if (HttpContext.Current.User.Identity.IsAuthenticated)
            {
                base.HandleUnauthorizedRequest(actionContext);
            }
            else
            {
                // user role was forbidden
                actionContext.Response = new HttpResponseMessage(HttpStatusCode.Forbidden);
            }
        }
    }
}