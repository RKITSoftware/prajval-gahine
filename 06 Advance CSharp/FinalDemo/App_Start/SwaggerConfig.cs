using FinalDemo.App_Start;
using Swashbuckle.Application;
using System.Reflection;
using System.Web;
using System.Web.Http;

[assembly: PreApplicationStartMethod(typeof(SwaggerConfig), "Register")]
namespace FinalDemo.App_Start
{
    public class SwaggerConfig
    {
        public static void Register()
        {
            Assembly thisAssembly = typeof(SwaggerConfig).Assembly;
            GlobalConfiguration.Configuration
                .EnableSwagger(c =>
                {
                    c.SingleApiVersion("v1", "FinalSampleDemo");
                })
                .EnableSwaggerUi();
        }
    }
}