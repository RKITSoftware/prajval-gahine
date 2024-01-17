using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Filters;
using System.Net.Http;
using System.Net;

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
            if(actionExecutedContext.Exception is NotImplementedException)
            {
                actionExecutedContext.Response = new HttpResponseMessage(HttpStatusCode.NotImplemented);
            }
        }
    }
}