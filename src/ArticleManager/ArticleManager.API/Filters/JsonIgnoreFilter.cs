using ArticleManager.Application.ExtensionMethods.String;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;
using System.Text.Json.Serialization;

namespace ArticleManager.API.Filters;

/// <summary>
/// Filter to ignore properties with <see cref="JsonIgnoreAttribute"/> in Swagger.
/// </summary>
public class JsonIgnoreFilter : ISchemaFilter
{
    /// <summary>
    /// Applies filter to schema.
    /// </summary>
    /// <param name="schema">Schema for API.</param>
    /// <param name="schemaFilterContext">Context filter for schema.</param>
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
