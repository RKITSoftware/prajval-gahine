using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace FilterDemo.filters
{
    public class GlobalHeaderFilter : IActionFilter
    {
        private string _hName;
        private string _hValue;


        public GlobalHeaderFilter()
        {
            _hName = "name";
            _hValue = "default";
        }

        public GlobalHeaderFilter(string name, string value)
        {
            _hName = name;
            _hValue = value;
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            context.HttpContext.Response.Headers.Add(_hName, _hValue);
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            //throw new NotImplementedException();
        }
    }
}
