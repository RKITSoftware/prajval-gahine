using FirmAdvanceDemo.Auth;
using Swashbuckle.Swagger;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Http.Filters;

namespace FirmAdvanceDemo.SwaggerRequirements
{
    /// <summary>
    /// Represents an operation filter for applying access token authentication requirements in Swagger documentation.
    /// </summary>
    public class AccessTokenAuthRequirements : IOperationFilter
    {
        /// <summary>
        /// Applies the access token authentication requirements to the Swagger operation.
        /// </summary>
        /// <param name="operation">The Swagger operation to apply the requirements to.</param>
        /// <param name="schemaRegistry">The schema registry.</param>
        /// <param name="apiDescription">The API description.</param>
        public void Apply(Operation operation, SchemaRegistry schemaRegistry, ApiDescription apiDescription)
        {
            Collection<FilterInfo> filterPipeline = apiDescription.ActionDescriptor.GetFilterPipeline();
            bool isAuthorized = filterPipeline
                .Select(filterInfo => filterInfo.Instance)
                .Any(filter => filter is AccessTokenAuthenticationAttribute);

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
                            description = "Bearer Authentication (jwt)"
                        }
                    );
            }
        }
    }
}
