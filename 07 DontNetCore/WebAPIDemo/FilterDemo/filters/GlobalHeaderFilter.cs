using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace FilterDemo.filters
{
    /// <summary>
    /// Gloabl filter class to add header
    /// </summary>
    public class GlobalHeaderFilter : IActionFilter
    {
        /// <summary>
        /// header name
        /// </summary>
        private string _hName;
        
        /// <summary>
        /// header value
        /// </summary>
        private string _hValue;

        /// <summary>
        /// Default constructor to initalize header name and value
        /// </summary>
        public GlobalHeaderFilter()
        {
            _hName = "name";
            _hValue = "default";
        }

        /// <summary>
        /// Parameterized constructor to initalize header name and value
        /// </summary>
        /// <param name="name">Header name</param>
        /// <param name="value">Header value</param>
        public GlobalHeaderFilter(string name, string value)
        {
            _hName = name;
            _hValue = value;
        }

        /// <summary>
        /// Append header info to response header
        /// </summary>
        /// <param name="context">ActionExecutedContext instance</param>
        public void OnActionExecuted(ActionExecutedContext context)
        {
            context.HttpContext.Response.Headers.Add(_hName, _hValue);
        }

        /// <summary>
        /// Method to be executed before action execution starts
        /// </summary>
        /// <param name="context">ActionExecutingContext instance</param>
        /// <exception cref="NotImplementedException"></exception>
        public void OnActionExecuting(ActionExecutingContext context)
        {
            throw new NotImplementedException();
        }
    }
}
