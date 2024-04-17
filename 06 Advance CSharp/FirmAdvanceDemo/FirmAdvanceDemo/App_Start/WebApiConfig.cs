using System.Configuration;
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
            //    defaults: new { ID = RouteParameter.Optional }
            //);

            string ConnString = ConfigurationManager.ConnectionStrings["connect-to-firmadvance2-db"].ConnectionString;
            System.Diagnostics.Debug.WriteLine(ConnString);

            InitializeDB.Init();

            GlobalConfiguration.Configuration.Formatters
                .Remove(GlobalConfiguration.Configuration.Formatters.XmlFormatter);
        }
    }
}
