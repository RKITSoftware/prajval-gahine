using FirmAdvanceDemo.Auth;
using Swashbuckle.Swagger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Description;
using System.Web.Http.Filters;
using System.Web.Http;
using System.Collections.ObjectModel;

namespace FirmAdvanceDemo.SwaggerRequirements
{
    public class BasicAuthRequirements : IOperationFilter
    {
        public void Apply(Operation operation, SchemaRegistry schemaRegistry, ApiDescription apiDescription)
        {
            Collection<FilterInfo> filterPipeline = apiDescription.ActionDescriptor.GetFilterPipeline();
            bool isAuthorized = filterPipeline
                .Select(filterInfo => filterInfo.Instance)
                .Any(filter => filter is BasicAuthenticationAttribute);

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
                            description = "Basic Authentication base64Encoded({username}:{password})"
                        }
                    );
            }
        }
    }
}