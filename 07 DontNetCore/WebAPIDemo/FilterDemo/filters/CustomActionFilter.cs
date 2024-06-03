using Microsoft.AspNetCore.Mvc.Filters;

namespace FilterDemo.filters
{
    /// <summary>
    /// Custom Action Filter - used to modify action specific data
    /// </summary>
    public class CustomActionFilter : IActionFilter
    {
        /// <summary>
        /// OnActionExecuting - before method execution
        /// </summary>
        /// <param name="context">ActionExecutingContext</param>
        public void OnActionExecuting(ActionExecutingContext context)
        {
            // Logic before the action executes
            Console.WriteLine("Action filter executing");
        }

        /// <summary>
        /// OnActionExecuted - after method execution
        /// </summary>
        /// <param name="context">ActionExecutedContext</param>
        public void OnActionExecuted(ActionExecutedContext context)
        {
            // Logic after the action executes
            Console.WriteLine("Action filter executed");
        }
    }

}
