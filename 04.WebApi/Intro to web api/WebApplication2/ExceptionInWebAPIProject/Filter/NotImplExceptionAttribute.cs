using System;
using System.Net;
using System.Net.Http;
using System.Web.Http.Filters;

namespace ExceptionInWebAPIProject.Filter
{
    /// <summary>
    /// Class to rasise exception event
    /// </summary>
    public class NotImplExceptionAttribute : ExceptionFilterAttribute
    {
        /// <summary>
        /// This method is executed when action throws NotImplementedException
        /// </summary>
        /// <param name="actionExecutedContext">action executed context after action is executed</param>
        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            if (actionExecutedContext.Exception is NotImplementedException)
            {
                actionExecutedContext.Response = new HttpResponseMessage(HttpStatusCode.NotImplemented);
            }
        }
    }
}