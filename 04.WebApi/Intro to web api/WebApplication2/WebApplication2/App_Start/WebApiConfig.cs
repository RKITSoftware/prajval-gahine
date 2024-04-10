using System.Web.Http;
using System.Web.Http.Cors;

namespace WebApplication2
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            //ICorsPolicyProvider cors = new DefaultCorsPolicyProvider();
            EnableCorsAttribute cors = new EnableCorsAttribute(origins: "*", headers: "*", methods: "*")
            {
                PreflightMaxAge = 10
            };
            config.EnableCors(cors);

            // Web API routes
            config.MapHttpAttributeRoutes();

            //config.Routes.MapHttpRoute(
            //    name: "DefaultApi",
            //    routeTemplate: "api/{controller}/{id}",
            //    defaults: new { id = RouteParameter.Optional }
            //);
        }
    }
}
