using FirmWebApiDemo.Exceptions;
using FirmWebApiDemo.Exceptions.CustomException;
using System;
using System.Net;
using System.Web.Http;
using System.Web.Http.Cors;

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

            config.EnableCors(new EnableCorsAttribute(origins: "http://127.0.0.1:5504", headers: "*", methods: "*"));
        }
    }
}
