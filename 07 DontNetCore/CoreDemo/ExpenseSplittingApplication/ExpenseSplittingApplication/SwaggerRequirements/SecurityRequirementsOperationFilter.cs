using Microsoft.AspNetCore.Authorization;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace ExpenseSplittingApplication.SwaggerRequirements
{
    public class SecurityRequirementsOperationFilter : IOperationFilter
    {
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
