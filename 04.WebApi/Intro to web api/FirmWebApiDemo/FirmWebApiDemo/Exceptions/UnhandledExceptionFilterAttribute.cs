
using System.Collections.Generic;
using System.Net.Http;
using System.Web;
using System.Web.Http.Filters;
using System;
using System.Net;
using System.Text;
using System.IO;

namespace FirmWebApiDemo.Exceptions
{
    public class UnhandledExceptionFilterAttribute : ExceptionFilterAttribute
    {
        private static readonly string LogFolderPath = HttpContext.Current.Server.MapPath(@"App_Data/logs/");

        private static readonly string DashLine = new string('-', 100);
        public UnhandledExceptionFilterAttribute() : base()
        {
        }

        private static Func<Exception, HttpRequestMessage, HttpResponseMessage> DefaultHandler = (exception, request) =>
        {

            if (exception == null)
            {
                return null;
            }

            string message = GetContentOf(exception);

            // log to file
            using (StreamWriter sw = new StreamWriter($"{LogFolderPath}{DateTime.Now.ToString("dd-MM-yyyy")}", true))
            {
                sw.WriteLine(DashLine);
                sw.WriteLine(DateTime.Now.ToString("hh:mm:ss tt"));
                sw.WriteLine(message);
                sw.WriteLine(DashLine);
            }

            HttpResponseMessage response = request.CreateResponse<string>(
                HttpStatusCode.InternalServerError,
                message
            );
            response.ReasonPhrase = exception.Message.Replace(Environment.NewLine, String.Empty);
            return response;
        };

        private static Func<Exception, string> GetContentOf = (exception) =>
        {
            if (exception == null)
            {
                return string.Empty;
            }

            StringBuilder result = new StringBuilder();
            result.AppendLine(exception.Message);
            result.AppendLine();

            Exception innerException = exception.InnerException;
            while (innerException != null)
            {
                result.AppendLine(innerException.Message);
                result.AppendLine();
                innerException = innerException.InnerException;
            }

            #if DEBUG
            result.AppendLine(exception.StackTrace);
            #endif

            return result.ToString();
        };

        protected Dictionary<Type, Tuple<HttpStatusCode?, Func<Exception, HttpRequestMessage, HttpResponseMessage>>> Handlers
        {
            get
            {
                return _filterHandlers;
            }
        }

        private readonly Dictionary<Type, Tuple<HttpStatusCode?, Func<Exception, HttpRequestMessage, HttpResponseMessage>>> _filterHandlers = new Dictionary<Type, Tuple<HttpStatusCode?, Func<Exception, HttpRequestMessage, HttpResponseMessage>>>();
        
        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            if(actionExecutedContext == null || actionExecutedContext.Exception == null)
            {
                return;
            }

            Type excetionType = actionExecutedContext.Exception.GetType();

            Tuple<HttpStatusCode?, Func<Exception, HttpRequestMessage, HttpResponseMessage>> registration = null;

            HttpResponseMessage response;

            if (this.Handlers.TryGetValue(excetionType, out registration))
            {
                HttpStatusCode? statusCode = registration.Item1;
                Func<Exception, HttpRequestMessage, HttpResponseMessage> handler = registration.Item2;

                 response = handler(
                    actionExecutedContext.Exception.GetBaseException(),
                    actionExecutedContext.Request
                );

                if (statusCode.HasValue)
                {
                    response.StatusCode = statusCode.Value;
                }
            }
            else
            {   // no handler provided for the thrown exceptionType
                response = DefaultHandler(
                    actionExecutedContext.Exception.GetBaseException(),
                    actionExecutedContext.Request
                );
            }
            actionExecutedContext.Response = response;
        }


        public UnhandledExceptionFilterAttribute Register<TException>(HttpStatusCode? statusCode)
        {
            Type exceptionType = typeof(TException);
            Tuple<HttpStatusCode?, Func<Exception, HttpRequestMessage, HttpResponseMessage>> item = Tuple.Create(statusCode, DefaultHandler);

            this.Handlers[exceptionType] = item;

            return this;
        }

        public UnhandledExceptionFilterAttribute Register<TException>(Func<Exception, HttpRequestMessage, HttpResponseMessage> handler)
            where TException : Exception
        {
            if(handler == null)
            {
                throw new ArgumentNullException("handler");
            }
            Type exceptionType = typeof(TException);
            Tuple<HttpStatusCode?, Func<Exception, HttpRequestMessage, HttpResponseMessage>> item = new Tuple<HttpStatusCode?, Func<Exception, HttpRequestMessage, HttpResponseMessage>>(
                null, handler
            );
            return this;
        }

        public UnhandledExceptionFilterAttribute Unregister<TException>()
        {
            Type exceptionType = typeof(TException);    
            this.Handlers.Remove(exceptionType);

            return this;
        }
    }
}