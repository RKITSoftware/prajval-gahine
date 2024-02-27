using FirmWebApiDemo.Exceptions;
using FirmWebApiDemo.Exceptions.CustomException;
using System;
using System.Net;
using System.Web;
using System.Web.Http;

namespace FirmWebApiDemo
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            config.Filters.Add(
                new UnhandledExceptionFilterAttribute()
                .Register<UsernameNotFoundException>(HttpStatusCode.NotFound)
                .Register<Exception>(HttpStatusCode.InternalServerError)
            );
        }
    }
}
