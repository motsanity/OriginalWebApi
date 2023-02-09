using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
namespace webapi.AppService.Middleware
{
    public class AddHeaderOperationFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            if (operation.Parameters == null)
                operation.Parameters = new List<OpenApiParameter>(); var parameter = new OpenApiParameter
                {
                    Name = "x-admin-id",
                    In = ParameterLocation.Header,
                    Description = "Admin ID header for the request",
                    Required = false,
                    Schema = new OpenApiSchema
                    {
                        Type = "string",
                        MinLength = 36,
                        MaxLength = 36
                    }
                }; operation.Parameters.Add(parameter);
        }
    }
}