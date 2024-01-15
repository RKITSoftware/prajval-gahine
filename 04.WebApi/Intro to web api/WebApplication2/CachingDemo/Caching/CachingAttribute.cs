using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Http.Filters;

namespace CachingDemo.Caching
{
    public class CachingAttribute : ActionFilterAttribute
    {
        public const int maxAge = 3600;
        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            //base.OnActionExecuted(actionExecutedContext);
            actionExecutedContext.Response.Headers.CacheControl = new CacheControlHeaderValue
            {
                MaxAge = TimeSpan.FromSeconds(CachingAttribute.maxAge),
                MustRevalidate = true,
                Public = false
            };
        }
    }
}