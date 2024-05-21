using Microsoft.OpenApi.Models;

namespace AuthService.API.ExtensionMethods.Swagger;

/// <summary>
/// Information to configure Swagger.
/// </summary>
public static class SwaggerInfo
{
    /// <summary>
    /// Project name of swagger file.
    /// </summary>
    public const string ProjectNameJson = "CoffeeBlog-AuthService-swagger.json";

    /// <summary>
    /// Route prefix of swagger file.
    /// </summary>
    public const string RoutePrefix = "swagger";

    /// <summary>
    /// Name of document title.
    /// </summary>
    public const string DocumentTitle = "CoffeeBlog - AuthService Documentation";

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

    /// <summary>
    /// Information of swagger website.
    /// </summary>
    /// <param name="version">Version of API.</param>
    /// <returns>Instance of <see cref="OpenApiInfo"/></returns>
    public static OpenApiInfo SwaggerDocumentInfo(OpenApiInfo version) => new()
    {
        Title = version.Title,
        Version = version.Version,
        Description = "Swagger of Web API of AuthService created for CoffeeBlog",
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