using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http.Filters;

namespace ExceptionInWebAPIProject.Filter
{
    /// <summary>
    /// Class to rasise exception event
    /// </summary>
    public class NotImplExceptionAttribute : ExceptionFilterAttribute
    {
        private static string ExceptionFolderPath = HttpContext.Current.Server.MapPath(@"App_Data/Exception");
        /// <summary>
        /// This method is executed when action throws NotImplementedException
        /// </summary>
        /// <param name="actionExecutedContext">action executed context after action is executed</param>
        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            if (actionExecutedContext.Exception is NotImplementedException)
            {
                DateTime date = DateTime.Now.AddDays(2);
                string TodayDate = date.ToString("yyyy-MM-dd");
                FileInfo ExFile = new FileInfo($"{ExceptionFolderPath}/{TodayDate}");
                if (!ExFile.Exists)
                {
                    ExFile.Create();
                }
                File.AppendAllText(ExFile.FullName, $"{HttpStatusCode.NotImplemented}, Not Implemented Exception, {date}\n");
                actionExecutedContext.Response = new HttpResponseMessage(HttpStatusCode.NotImplemented);
            }
        }
    }
}