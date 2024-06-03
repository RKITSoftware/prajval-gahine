using Microsoft.AspNetCore.Mvc.Filters;

namespace FilterDemo.filters
{
    public class CustomResultFilter : IResultFilter
    {
        public void OnResultExecuting(ResultExecutingContext context)
        {
            // Logic before the result is executed
            Console.WriteLine("Result filter executing");
        }

        public void OnResultExecuted(ResultExecutedContext context)
        {
            // Logic after the result is executed
            Console.WriteLine("Result filter executed");
        }
    }

}
