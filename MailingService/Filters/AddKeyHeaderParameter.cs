using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace MailingService.Filters
{
    public class AddKeyHeaderParameter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            if (context.MethodInfo.Name == "SendEmail")
            {
                var parameter = new OpenApiParameter
                {
                    Name = "key",
                    In = ParameterLocation.Header,
                    Required = true,
                    Schema = new OpenApiSchema { Type = "string" }
                };

                operation.Parameters ??= new List<OpenApiParameter>();
                operation.Parameters.Add(parameter);
            }
        }
    }
}
