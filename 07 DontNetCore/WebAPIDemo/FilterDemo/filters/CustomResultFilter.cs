using Microsoft.AspNetCore.Mvc.Filters;

namespace FilterDemo.filters
{
    /// <summary>
    /// Result filter
    /// </summary>
    public class CustomResultFilter : IResultFilter
    {
        /// <summary>
        /// Method executes before result execution
        /// </summary>
        /// <param name="context"></param>
        public void OnResultExecuting(ResultExecutingContext context)
        {
            // Logic before the result is executed
            Console.WriteLine("Result filter executing");
        }
        /// <summary>
        /// Method executes after result execution
        /// </summary>
        /// <param name="context"></param>
        public void OnResultExecuted(ResultExecutedContext context)
        {
            // Logic after the result is executed
            Console.WriteLine("Result filter executed");
        }
    }

}
