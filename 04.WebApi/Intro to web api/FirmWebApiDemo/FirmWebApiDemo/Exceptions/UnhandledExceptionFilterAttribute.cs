
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Http.Filters;

namespace FirmWebApiDemo.Exceptions
{
    /// <summary>
    /// Exception Filter attribute that helps to handle any exception globally
    /// </summary>
    public class UnhandledExceptionFilterAttribute : ExceptionFilterAttribute
    {
        /// <summary>
        /// Folder path to exception log folder
        /// </summary>
        private static readonly string LogFolderPath = HttpContext.Current.Server.MapPath(@"App_Data/logs/");

        /// <summary>
        /// Dash delimiter for seperating exception in log files
        /// </summary>
        private static readonly string DashLine = new string('-', 100);

        /// <summary>
        /// UnhandledExceptionFilterAttribute default constructor
        /// </summary>
        public UnhandledExceptionFilterAttribute() : base()
        {
        }

        /// <summary>
        /// Default handler to execute when an exception is throw which is not binded to any other handler (except default handler)
        /// </summary>
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
                exception.Message
            );
            response.ReasonPhrase = exception.Message.Replace(Environment.NewLine, String.Empty);
            return response;
        };

        /// <summary>
        /// GetContentOf method to return whole content of exception (including it's nested inner exceptions)
        /// </summary>
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

        /// <summary>
        /// Handlers property contains mapping of exception type and handler to execute when that exception is thrown
        /// </summary>
        protected Dictionary<Type, Tuple<HttpStatusCode?, Func<Exception, HttpRequestMessage, HttpResponseMessage>>> Handlers
        {
            get
            {
                return _filterHandlers;
            }
        }

        /// <summary>
        /// _filterHandlers field contains mapping of exception type and handler to execute when that exception is thrown
        /// </summary>
        private readonly Dictionary<Type, Tuple<HttpStatusCode?, Func<Exception, HttpRequestMessage, HttpResponseMessage>>> _filterHandlers = new Dictionary<Type, Tuple<HttpStatusCode?, Func<Exception, HttpRequestMessage, HttpResponseMessage>>>();

        /// <summary>
        /// Mehtod to be executed when an exception is thrown and it executes appropriate handler based on thrown exception
        /// </summary>
        /// <param name="actionExecutedContext"></param>
        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            if (actionExecutedContext == null || actionExecutedContext.Exception == null)
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

        /// <summary>
        /// Method to register an exception and it's corresponding status code and handler
        /// </summary>
        /// <typeparam name="TException">Exception</typeparam>
        /// <param name="statusCode">HttpStatusCode</param>
        /// <returns>Instance of UnhandledExceptionFilterAttribute class</returns>

        public UnhandledExceptionFilterAttribute Register<TException>(HttpStatusCode? statusCode)
        {
            Type exceptionType = typeof(TException);
            Tuple<HttpStatusCode?, Func<Exception, HttpRequestMessage, HttpResponseMessage>> item = Tuple.Create(statusCode, DefaultHandler);

            this.Handlers[exceptionType] = item;

            return this;
        }

        /// <summary>
        /// Method to register an exception and it's corresponding handler
        /// </summary>
        /// <typeparam name="TException">Exception</typeparam>
        /// <param name="handler">Handler to be executed when this Exception is thrown</param>
        /// <returns>Instance of UnhandledExceptionFilterAttribute</returns>
        /// <exception cref="ArgumentNullException">ArgumentNullException</exception>
        public UnhandledExceptionFilterAttribute Register<TException>(Func<Exception, HttpRequestMessage, HttpResponseMessage> handler)
            where TException : Exception
        {
            if (handler == null)
            {
                throw new ArgumentNullException("handler");
            }
            Type exceptionType = typeof(TException);
            Tuple<HttpStatusCode?, Func<Exception, HttpRequestMessage, HttpResponseMessage>> item = new Tuple<HttpStatusCode?, Func<Exception, HttpRequestMessage, HttpResponseMessage>>(
                null, handler
            );
            return this;
        }

        /// <summary>
        /// Method to unregister a specific Exception
        /// </summary>
        /// <typeparam name="TException">Exception</typeparam>
        /// <returns>Instance of UnhandledExceptionFilterAttribute</returns>
        public UnhandledExceptionFilterAttribute Unregister<TException>()
        {
            Type exceptionType = typeof(TException);
            this.Handlers.Remove(exceptionType);

            return this;
        }
    }
}