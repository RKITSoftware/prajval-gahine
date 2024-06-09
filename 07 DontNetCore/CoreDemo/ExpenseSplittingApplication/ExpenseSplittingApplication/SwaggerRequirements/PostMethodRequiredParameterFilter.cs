using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Collections.Generic;
using System.Linq;

namespace ExpenseSplittingApplication.SwaggerRequirements
{
    public class PostMethodRequiredParameterFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            if (operation.RequestBody != null && operation.RequestBody.Content.ContainsKey("application/json"))
            {
                var jsonSchema = operation.RequestBody.Content["application/json"].Schema;

                if (jsonSchema != null)
                {
                    // Resolve schema reference
                    if (jsonSchema.Reference != null)
                    {
                        var schemaReferenceId = jsonSchema.Reference.Id;
                        jsonSchema = context.SchemaRepository.Schemas[schemaReferenceId];
                    }

                    if (jsonSchema.Properties.Count > 0)
                    {
                        if (context.ApiDescription.HttpMethod?.ToLower() == "put")
                        {
                            // Ensure r01101 is not read-only for PUT requests
                            if (jsonSchema.Properties.ContainsKey("r01101"))
                            {
                                jsonSchema.Properties["r01101"].ReadOnly = false;
                            }
                        }
                        else if (context.ApiDescription.HttpMethod?.ToLower() == "post")
                        {
                            // Set r01101 as read-only for POST requests
                            if (jsonSchema.Properties.ContainsKey("r01101"))
                            {
                                jsonSchema.Properties["r01101"].ReadOnly = true;
                            }
                        }
                    }
                }
            }
        }
        public void Apply2(OpenApiOperation operation, OperationFilterContext context)
        {
            string? httpMethod = context.ApiDescription.HttpMethod;
            string? controllerName = context.ApiDescription.ActionDescriptor.RouteValues["controller"];

            OpenApiParameter? parameter;

            if (controllerName == "CLUSR01")
            {
                if (httpMethod?.ToLower() == "post")
                {
                    /*
                    OpenApiRequestBody requestBody = operation.RequestBody;
                    if(requestBody != null)
                    {
                        foreach (KeyValuePair<string, OpenApiMediaType> content in requestBody.Content)
                        {
                           OpenApiSchema schema =  content.Value.Schema;
                            if (schema.Properties.ContainsKey("R01F01"))
                            {
                                schema.Properties.Remove("R01F01");
                            }

                            if (schema.Properties.ContainsKey("R01F03"))
                            {
                                schema.Properties["R01F03"].Required = new HashSet<string> { "R01F03 prajval" };
                            }
                        }
                    }
                    */

                    // Iterate through all the content types in the request body
                    foreach (var content in operation.RequestBody.Content)
                    {
                        var jsonSchema = content.Value.Schema;

                        if (jsonSchema != null)
                        {
                            // Remove the r01101 parameter from the body schema
                            if (jsonSchema.Properties.ContainsKey("r01101"))
                            {
                                jsonSchema.Properties.Remove("r01101");
                            }

                            // Mark R01103 as required
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

                    /*
                    // remove the userID parameter
                    parameter = operation.Parameters.FirstOrDefault(p => p.Name == "R01F01");
                    operation.Parameters.Remove(parameter);

                    // mark password as required in case of post
                    parameter = operation.Parameters.FirstOrDefault(p => p.Name == "R01F03");
                    if (parameter != null)
                    {
                        parameter.Required = true;
                    }
                    */
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