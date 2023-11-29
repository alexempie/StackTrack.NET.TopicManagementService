using Humanizer;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace TopicManagementService.API.Swagger;

public class LowercaseRouteParameterFilter : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        if (operation.Parameters != null)
        {
            foreach (var parameter in operation.Parameters)
            {
                parameter.Name = parameter.Name.Camelize();;
            }
        }
    }
}