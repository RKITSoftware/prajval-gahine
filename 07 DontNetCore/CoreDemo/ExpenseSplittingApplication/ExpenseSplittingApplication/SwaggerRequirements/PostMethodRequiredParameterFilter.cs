using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace ExpenseSplittingApplication.SwaggerRequirements
{
    public class PostMethodRequiredParameterFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            string? httpMethod = context.ApiDescription.HttpMethod;
            string? controllerName = context.ApiDescription.ActionDescriptor.RouteValues["controller"];

            OpenApiParameter? parameter;

            if (controllerName == "CLUSR01")
            {
                if (httpMethod?.ToLower() == "post")
                {
                    // remove the userID parameter
                    parameter = operation.Parameters.FirstOrDefault(p => p.Name == "R01F01");
                    operation.Parameters.Remove(parameter);

                    // mark password as required in case of post
                    parameter = operation.Parameters.FirstOrDefault(p => p.Name == "R01F03");
                    if (parameter != null)
                    {
                        parameter.Required = true;
                    }
                }

                if (httpMethod?.ToLower() == "put")
                {
                    // remove the password parameter
                    parameter = operation.Parameters.FirstOrDefault(p => p.Name == "R01F03");
                    operation.Parameters.Remove(parameter);

                    // mark userID as required
                    parameter = operation.Parameters.FirstOrDefault(p => p.Name == "R01F01");
                    if (parameter != null)
                    {
                        parameter.Required = true;
                    }
                }
            }
        }
    }
}
//&& p.In == ParameterLocation.Query