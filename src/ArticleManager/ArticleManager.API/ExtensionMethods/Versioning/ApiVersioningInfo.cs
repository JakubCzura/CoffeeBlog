using Asp.Versioning;
using Microsoft.OpenApi.Models;

namespace ArticleManager.API.ExtensionMethods.Versioning;

/// <summary>
/// Information about API versioning.
/// </summary>
public static class ApiVersioningInfo
{
    private const string _title = "CoffeeBlog - ArticleManager API";

    /// <summary>
    /// Version 1.0 of API.
    /// </summary>
    public const string Version_1_0 = "1.0";

    /// <summary>
    /// Current version of API.
    /// </summary>
    public static ApiVersion CurrentVersion { get; } = new(1, 0);

    /// <summary>
    /// List of available API versions.
    /// </summary>
    public static readonly IReadOnlyList<OpenApiInfo> AvailableVersions =
    [
        new()
        {
            Title = _title,
            Version = $"v{Version_1_0}"
        }
    ];
}