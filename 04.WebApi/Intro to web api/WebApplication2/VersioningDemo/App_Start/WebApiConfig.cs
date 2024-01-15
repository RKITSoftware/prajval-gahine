using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Dispatcher;
using VersioningDemo.Versioning;

namespace VersioningDemo
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();


           // config.Routes.MapHttpRoute(
           //     name: "EmployeeVersion1",
           //     routeTemplate: "api/v1/employee/{id}",
           //     defaults: new { controller = "EmployeeV1", id = RouteParameter.Optional }
           // );

           // config.Routes.MapHttpRoute(
           //     name: "EmployeeVersion2",
           //     routeTemplate: "api/v2/employee/{id}",
           //     defaults: new { controller = "EmployeeV2", id = RouteParameter.Optional }
           //);


            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional}
            );

            //config.Services.Replace(typeof(IHttpControllerSelector), new CustomControllerSelector(config));
            config.Services.Replace(typeof(IHttpControllerSelector), new ControllerSelectorFromCustomHeader(config));
        }
    }
}
