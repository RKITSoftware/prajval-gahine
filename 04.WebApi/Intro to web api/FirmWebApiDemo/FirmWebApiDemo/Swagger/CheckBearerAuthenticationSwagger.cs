using FirmWebApiDemo.Authentication;
using Swashbuckle.Swagger;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Http.Filters;

namespace FirmWebApiDemo.Swagger
{
    /// <summary>
    /// Class to filter out http action with BearerAuthenticationAttribute attribute
    /// </summary>
    public class CheckBearerAuthenticationSwagger : IOperationFilter
    {
        /// <summary>
        /// Applies parameter on BearerAuthenticationAttribute attributed actions
        /// </summary>
        /// <param name="operation">Options</param>
        /// <param name="schemaRegistry">Schema Registry</param>
        /// <param name="apiDescription">Api Description</param>
        public void Apply(Operation operation, SchemaRegistry schemaRegistry, ApiDescription apiDescription)
        {
            Collection<FilterInfo> filterPipeline = apiDescription.ActionDescriptor.GetFilterPipeline();
            bool isAuthorized = filterPipeline
                .Select(filterInfo => filterInfo.Instance)
                .Any(filter => filter is BearerAuthenticationAttribute);

            bool isAnonymous = apiDescription.ActionDescriptor.GetCustomAttributes<AllowAnonymousAttribute>().Any();

            if (isAuthorized && !isAnonymous)
            {
                if (operation.parameters == null)
                {
                    operation.parameters = new List<Parameter>();
                }
                operation.parameters.Add(
                        new Parameter
                        {
                            name = "Authorization",
                            @in = "header",
                            type = "string",
                            required = true,
                            description = "Bearer Authentication jwt"
                        }
                    );
            }
        }
    }
}