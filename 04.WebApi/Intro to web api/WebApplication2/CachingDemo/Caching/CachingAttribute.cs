using System;
using System.Net.Http.Headers;
using System.Web.Http.Filters;

namespace CachingDemo.Caching
{
    /// <summary>
    /// Class to Intercept request flow and add caching info to reponse
    /// </summary>
    public class CachingAttribute : ActionFilterAttribute
    {
        /// <summary>
        /// max-age of reponse to be cache at client side from time when response was generated
        /// </summary>
        public const int maxAge = 3600;


        /// <summary>
        /// Method to Intercept request flow after action is invoked and add caching info to reponse
        /// </summary>
        /// <param name="actionExecutedContext"></param>
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