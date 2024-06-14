using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Collections.Generic;
using System.Linq;

namespace ExpenseSplittingApplication.SwaggerRequirements
{
    /// <summary>
    /// Operation filter for modifying OpenAPI schema based on HTTP method and controller context.
    /// </summary>
    public class PostMethodRequiredParameterFilter : IOperationFilter
    {
        /// <summary>
        /// Applies modifications to the OpenAPI operation based on HTTP method and request body schema.
        /// </summary>
        /// <param name="operation">The OpenAPI operation being modified.</param>
        /// <param name="context">The context of the operation filter.</param>
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            if (operation.RequestBody != null && operation.RequestBody.Content.ContainsKey("application/json"))
            {
                var jsonSchema = operation.RequestBody.Content["application/json"].Schema;

                if (jsonSchema != null)
                {
                    // Resolve schema reference if present
                    if (jsonSchema.Reference != null)
                    {
                        var schemaReferenceId = jsonSchema.Reference.Id;
                        jsonSchema = context.SchemaRepository.Schemas[schemaReferenceId];
                    }

                    // Modify schema properties based on HTTP method
                    if (jsonSchema.Properties.Count > 0)
                    {
                        if (context.ApiDescription.HttpMethod?.ToLower() == "put")
                        {
                            // Ensure 'r01101' is not read-only for PUT requests
                            if (jsonSchema.Properties.ContainsKey("r01101"))
                            {
                                jsonSchema.Properties["r01101"].ReadOnly = false;
                            }
                        }
                        else if (context.ApiDescription.HttpMethod?.ToLower() == "post")
                        {
                            // Set 'r01101' as read-only for POST requests
                            if (jsonSchema.Properties.ContainsKey("r01101"))
                            {
                                jsonSchema.Properties["r01101"].ReadOnly = true;
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Applies additional modifications to the OpenAPI operation based on HTTP method and controller context.
        /// </summary>
        /// <param name="operation">The OpenAPI operation being modified.</param>
        /// <param name="context">The context of the operation filter.</param>
        public void Apply2(OpenApiOperation operation, OperationFilterContext context)
        {
            string? httpMethod = context.ApiDescription.HttpMethod;
            string? controllerName = context.ApiDescription.ActionDescriptor.RouteValues["controller"];

            OpenApiParameter? parameter;

            if (controllerName == "CLUSR01")
            {
                if (httpMethod?.ToLower() == "post")
                {
                    // Iterate through all content types in the request body
                    foreach (var content in operation.RequestBody.Content)
                    {
                        var jsonSchema = content.Value.Schema;

                        if (jsonSchema != null)
                        {
                            // Remove the 'r01101' parameter from the body schema
                            if (jsonSchema.Properties.ContainsKey("r01101"))
                            {
                                jsonSchema.Properties.Remove("r01101");
                            }

                            // Mark 'R01103' as required
                            if (jsonSchema.Properties.ContainsKey("R01103"))
                            {
                                if (jsonSchema.Required == null)
                                {
                                    jsonSchema.Required = new HashSet<string>();
                                }
                                jsonSchema.Required.Add("R01103");
                            }
                        }
                    }
                }

                if (httpMethod?.ToLower() == "put")
                {
                    // Remove the 'R01F03' parameter
                    parameter = operation.Parameters.FirstOrDefault(p => p.Name == "R01F03");
                    operation.Parameters.Remove(parameter);

                    // Mark 'R01F01' as required
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
