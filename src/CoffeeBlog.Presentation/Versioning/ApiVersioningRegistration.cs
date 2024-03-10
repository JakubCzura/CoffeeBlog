using Asp.Versioning;

namespace CoffeeBlog.Presentation.Versioning;

/// <summary>
/// Extension methods to register API versioning.
/// </summary>
public static class ApiVersioningRegistration
{
    /// <summary>
    /// Configures API versioning.
    /// </summary>
    /// <param name="services">Reference to <see cref="IServiceCollection"/>.</param>
    /// <returns><paramref name="services"/> with configured API versioning.</returns>
    public static IServiceCollection AddApiVersion(this IServiceCollection services)
    {
        services.AddApiVersioning(config =>
        {
            config.AssumeDefaultVersionWhenUnspecified = true;
            config.DefaultApiVersion = ApiVersioningInfo.CurrentVersion;
            config.ReportApiVersions = true;
            config.ApiVersionReader = new UrlSegmentApiVersionReader();
        });
        //.AddMvc();

        return services;
    }
}