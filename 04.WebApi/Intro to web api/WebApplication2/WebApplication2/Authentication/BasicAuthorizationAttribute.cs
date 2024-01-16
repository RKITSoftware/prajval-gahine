using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;

namespace WebApplication2.Authentication
{
    public class BasicAuthorizationAttribute : AuthorizeAttribute 
    {
        protected override void HandleUnauthorizedRequest(HttpActionContext actionContext)
        {
            //if (HttpContext.Current.User.Identity.IsAuthenticated)
            //{
                base.HandleUnauthorizedRequest(actionContext);
            //}
            //else
            //{
            //    // user role was forbidden
            //    actionContext.Response = new HttpResponseMessage(HttpStatusCode.Forbidden);
            //}
        }
    }
}