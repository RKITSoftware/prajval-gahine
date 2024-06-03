using Microsoft.AspNetCore.Mvc.Filters;

namespace FilterDemo.filters
{
    /// <summary>
    /// Custom Resource Filter - manipulate data associated with resouce being accessed
    /// </summary>
    public class CustomResourceFilter : IResourceFilter
    {
        /// <summary>
        /// OnResourceExecuting - executed before action method
        /// </summary>
        /// <param name="context">ResourceExecutingContext instance</param>
        public void OnResourceExecuting(ResourceExecutingContext context)
        {
            // Logic before the rest of the pipeline
            Console.WriteLine("Resource filter executing");
        }

        /// <summary>
        /// OnResourceExecuted - executed after action method
        /// </summary>
        /// <param name="context">ResourceExecutedContext instance</param>
        public void OnResourceExecuted(ResourceExecutedContext context)
        {
            // Logic after the rest of the pipeline
            Console.WriteLine("Resource filter executed");
        }
    }

}
