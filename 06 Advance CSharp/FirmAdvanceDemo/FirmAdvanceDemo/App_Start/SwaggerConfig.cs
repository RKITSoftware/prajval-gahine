using FirmAdvanceDemo;
using FirmAdvanceDemo.Swagger;
using Swashbuckle.Application;
using System.Web.Http;
using WebActivatorEx;

[assembly: PreApplicationStartMethod(typeof(SwaggerConfig), "Register")]

namespace FirmAdvanceDemo
{
    public class SwaggerConfig
    {
        public static void Register()
        {
            var thisAssembly = typeof(SwaggerConfig).Assembly;

            GlobalConfiguration.Configuration
                .EnableSwagger(c =>
                    {
                        c.SingleApiVersion("v1", "FirmAdvanceDemo");
                        c.ApiKey("apiKey")
                            .Description("Access Token")
                            .Name("Authorization")
                            .In("header");
                        c.DescribeAllEnumsAsStrings();
                        c.OperationFilter<BasicAuthRequirements>();
                    })
                .EnableSwaggerUi(c =>
                    {
                        c.InjectJavaScript(thisAssembly, "FirmAdvanceDemo.Swagger.Content.SwaggerScript.js");
                        c.EnableApiKeySupport("Authorization", "header");
                    });
        }
    }
}
