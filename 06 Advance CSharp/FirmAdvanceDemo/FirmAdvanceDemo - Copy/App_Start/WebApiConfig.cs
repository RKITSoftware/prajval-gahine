﻿using System.Configuration;
using System.Web.Http;

namespace FirmAdvanceDemo
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            //config.Routes.MapHttpRoute(
            //    name: "DefaultApi",
            //    routeTemplate: "api/{controller}/{id}",
            //    defaults: new { id = RouteParameter.Optional }
            //);

            string ConnString = ConfigurationManager.ConnectionStrings["MyDB"].ConnectionString;
            System.Diagnostics.Debug.WriteLine(ConnString);

            CreateTables.CreateTablesMethod();

            GlobalConfiguration.Configuration.Formatters
                .Remove(GlobalConfiguration.Configuration.Formatters.XmlFormatter);
        }
    }
}