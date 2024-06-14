using Microsoft.AspNetCore.Authorization;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Collections.Generic;
using System.Linq;

namespace ExpenseSplittingApplication.SwaggerRequirements
{
    /// <summary>
    /// Operation filter for adding security requirements to Swagger operations based on Authorize attributes.
    /// </summary>
    public class SecurityRequirementsOperationFilter : IOperationFilter
    {
        /// <summary>
        /// Applies bearer token security requirements to the OpenAPI operation if the method is authorized.
        /// </summary>
        /// <param name="operation">The OpenAPI operation being modified.</param>
        /// <param name="context">The context of the operation filter.</param>
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            IEnumerable<AuthorizeAttribute> authAttributes = context.MethodInfo
                .GetCustomAttributes(true)
                .OfType<AuthorizeAttribute>()
                .Distinct();

            if (authAttributes.Any())
            {
                if (operation.Security == null)
                    operation.Security = new List<OpenApiSecurityRequirement>();

                // Define bearer token security scheme
                OpenApiSecurityScheme bearerSecurityScheme = new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "bearer" }
                };

                operation.Security.Add(new OpenApiSecurityRequirement
                {
                    [bearerSecurityScheme] = new List<string>()
                });
            }
        }

        /// <summary>
        /// Applies basic security requirements to the OpenAPI operation if the method is authorized.
        /// </summary>
        /// <param name="operation">The OpenAPI operation being modified.</param>
        /// <param name="context">The context of the operation filter.</param>
        public void Apply2(OpenApiOperation operation, OperationFilterContext context)
        {
            IEnumerable<AuthorizeAttribute> authAttributes = context.MethodInfo
                .GetCustomAttributes(true)
                .OfType<AuthorizeAttribute>()
                .Distinct();

            if (authAttributes.Any())
            {
                if (operation.Security == null)
                    operation.Security = new List<OpenApiSecurityRequirement>();

                // Define basic security scheme
                OpenApiSecurityScheme basicSecurityScheme = new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "basic" }
                };

                operation.Security.Add(new OpenApiSecurityRequirement
                {
                    [basicSecurityScheme] = new List<string>()
                });
            }
        }
    }
}
