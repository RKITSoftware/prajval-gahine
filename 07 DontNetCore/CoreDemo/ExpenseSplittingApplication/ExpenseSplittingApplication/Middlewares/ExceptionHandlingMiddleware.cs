using ExpenseSplittingApplication.BL.Common.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System;
using Newtonsoft.Json;
using System.Net;
using ExpenseSplittingApplication.BL.Common.Service;

namespace ExpenseSplittingApplication.Middlewares
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILoggerService _loggerService;

        public ExceptionHandlingMiddleware(RequestDelegate next, ApplicationLoggerService loggerService)
        {
            _next = next;
            _loggerService = loggerService;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _loggerService.Error(ex.ToString());

                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                var result = JsonConvert.SerializeObject(new
                {
                    error = "An unexpected error occurred. Please try again later.",
                    details = ex.Message
                });
                await context.Response.WriteAsync(result);
            }
        }
    }
}
