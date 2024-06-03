using Microsoft.AspNetCore.Mvc.Filters;

namespace FilterDemo.filters
{
    /// <summary>
    /// Custom Exception Filterc class
    /// </summary>
    public class CustomExceptionFilter : IExceptionFilter
    {
        /// <summary>
        /// Method is executed if any exception occurs
        /// </summary>
        /// <param name="context"></param>
        public void OnException(ExceptionContext context)
        {
            Console.WriteLine("ExceptionFilter");
        }
    }
}
