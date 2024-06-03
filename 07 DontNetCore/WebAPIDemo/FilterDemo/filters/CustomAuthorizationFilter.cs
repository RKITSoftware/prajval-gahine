using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace FilterDemo.filters
{
    /// <summary>
    /// Custom Authorization Filter
    /// </summary>
    public class CustomAuthorizationFilter : IAuthorizationFilter
    {
        /// <summary>
        /// Method executed at start of filter execution pipeline
        /// </summary>
        /// <param name="context"></param>
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            // Custom authorization logic
            //if (!context.HttpContext.User.Identity.IsAuthenticated)
            //{
            //    context.Result = new UnauthorizedResult();
            //}
            Console.WriteLine("AuthorizationFilter");
        }
    }
}
