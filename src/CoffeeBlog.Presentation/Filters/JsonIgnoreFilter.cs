using CoffeeBlog.Application.ExtensionMethods.String;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;
using System.Text.Json.Serialization;

namespace CoffeeBlog.Presentation.Filters;

public class JsonIgnoreFilter : ISchemaFilter
{
    public void Apply(OpenApiSchema schema,
                      SchemaFilterContext schemaFilterContext)
    {
        if (schema.Properties.Count == 0)
        {
            return;
        }

        schemaFilterContext.Type.GetProperties()
                                .Where(propertyInfo => propertyInfo.GetCustomAttribute<JsonIgnoreAttribute>() is not null)
                                .Select(propertyInfo => propertyInfo.GetCustomAttribute<JsonPropertyNameAttribute>()?.Name ?? propertyInfo.Name.ToCamelCase())
                                .ToList()
                                .ForEach(propertyName =>
                                {
                                    schema.Properties.Remove(propertyName);
                                });
    }
}