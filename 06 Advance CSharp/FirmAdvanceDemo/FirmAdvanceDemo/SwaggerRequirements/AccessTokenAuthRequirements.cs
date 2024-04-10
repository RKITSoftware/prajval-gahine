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
    class AccessTokenAuthRequirements : IOperationFilter
    {
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