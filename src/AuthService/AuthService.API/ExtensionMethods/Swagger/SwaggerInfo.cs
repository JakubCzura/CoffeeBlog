using Microsoft.OpenApi.Models;

namespace AuthService.API.ExtensionMethods.Swagger;

public static class SwaggerInfo
{
    /// <summary>
    /// Project name of swagger file.
    /// </summary>
    public const string ProjectNameJson = "CoffeeBlog-swagger.json";

    /// <summary>
    /// Route prefix of swagger file.
    /// </summary>
    public const string RoutePrefix = "swagger";

    /// <summary>
    /// Name of document title.
    /// </summary>
    public const string DocumentTitle = "CoffeeBlog API Documentation";

    /// <summary>
    /// Route template to use swagger.
    /// </summary>
    public static string RouteTemplate => $"{RoutePrefix}/{{documentName}}/{ProjectNameJson}";

    /// <summary>
    /// Endpoint URL of swagger.
    /// </summary>
    /// <param name="version">Version of API.</param>
    /// <returns>Endpoint URL to use swagger.</returns>
    public static string EndpointUrl(OpenApiInfo version) => $"/{RoutePrefix}/{version.Version}/{ProjectNameJson}";

    /// <summary>
    /// Name of swagger's endpoint for specified API version.
    /// </summary>
    /// <param name="version">Version of API.</param>
    /// <returns>Name of endpoint to use swagger.</returns>
    public static string EndpointName(OpenApiInfo version) => $"API {version.Version}";

    public static OpenApiInfo SwaggerDocumentInfo(OpenApiInfo version) => new()
    {
        Title = version.Title,
        Version = version.Version,
        Description = "Swagger of Web API of CoffeeBlog delivered to interact with Blazor UI",
        Contact = new OpenApiContact
        {
            Name = "Jakub Czura",
            Url = new("https://github.com/jakubczura")
        },
        License = new OpenApiLicense
        {
            Name = "MIT License",
            Url = new Uri("https://opensource.org/licenses/MIT")
        },
    };
}