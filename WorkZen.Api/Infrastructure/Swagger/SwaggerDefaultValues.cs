using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using Microsoft.AspNetCore.Mvc.ApiExplorer;

namespace WorkZen.Api.Infrastructure.Swagger;

public class SwaggerDefaultValues : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        var apiDescription = context.ApiDescription;

        operation.Deprecated |= apiDescription.IsDeprecated();

        if (operation.Parameters == null)
            return;

        foreach (var parameter in operation.Parameters)
        {
            var description = apiDescription.ParameterDescriptions
                .First(p => p.Name == parameter.Name);

            parameter.Description ??= description.ModelMetadata?.Description;

            if (parameter.Schema.Default == null &&
                description.DefaultValue != null)
            {
                parameter.Schema.Default = OpenApiAnyFactory.CreateFromJson(
                    description.DefaultValue.ToString()!
                );
            }
        }
    }
}