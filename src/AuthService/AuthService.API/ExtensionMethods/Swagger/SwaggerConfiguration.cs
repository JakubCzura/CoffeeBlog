using Asp.Versioning;
using AuthService.API.ExtensionMethods.Versioning;
using AuthService.API.Filters;
using AuthService.Application.ExtensionMethods.LayerRegistration;
using AuthService.Domain.Entities.Basics;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using Swashbuckle.AspNetCore.SwaggerUI;
using System.Reflection;

namespace AuthService.API.ExtensionMethods.Swagger;

/// <summary>
/// Configuration of Swagger.
/// </summary>
public static class SwaggerConfiguration
{
    private static string GetProjectXmlDocumentation(Assembly projectAssembly)
        => Path.Combine(AppContext.BaseDirectory, $"{projectAssembly.GetName().Name}.xml");

    private static void AddProjectsXmlDocumentations(this SwaggerGenOptions swaggerGenOptions,
                                                     params Assembly[] projectsAssemblies)
        => projectsAssemblies.ToList()
                             .ForEach(projectAssembly =>
                             {
                                 string xmlDocumentationPath = GetProjectXmlDocumentation(projectAssembly);
                                 swaggerGenOptions.IncludeXmlComments(xmlDocumentationPath);
                             });

    private static void AddSecurity(this SwaggerGenOptions swaggerGenOptions)
    {
        swaggerGenOptions.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, new OpenApiSecurityScheme
        {
            In = ParameterLocation.Header,
            Description = $"JWT authorization. Please enter JWT below:",
            Name = "Authorization",
            Type = SecuritySchemeType.Http,
            BearerFormat = "JWT",
            Scheme = JwtBearerDefaults.AuthenticationScheme
        });
        swaggerGenOptions.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Id = JwtBearerDefaults.AuthenticationScheme,
                        Type = ReferenceType.SecurityScheme
                    }
                },
                Array.Empty<string>()
            }
        });
    }

    /// <summary>
    /// Registers Swagger in dependency injection services.
    /// </summary>
    /// <param name="services">Collection of dependency injection services.</param>
    /// <returns>Reference to <paramref name="services"/></returns>
    public static IServiceCollection AddSwagger(this IServiceCollection services)
    {
        services.AddSwaggerGen(swaggerGenOptions =>
        {
            ApiVersioningInfo.AvailableVersions.ToList()
                                               .ForEach(version => swaggerGenOptions.SwaggerDoc(version.Version, SwaggerInfo.SwaggerDocumentInfo(version)));

            swaggerGenOptions.AddProjectsXmlDocumentations(Assembly.GetExecutingAssembly(),
                                                           Assembly.GetAssembly(typeof(DbEntityBase))!,
                                                           Assembly.GetAssembly(typeof(ApplicationRegistration))!);

            swaggerGenOptions.SchemaFilter<JsonIgnoreFilter>();

            swaggerGenOptions.AddSecurity();

            swaggerGenOptions.UseAllOfToExtendReferenceSchemas();

            swaggerGenOptions.CustomOperationIds(x => $"{x.ActionDescriptor.RouteValues["controller"]}_{x.HttpMethod}");

            swaggerGenOptions.DocInclusionPredicate((version, description) =>
            {
                if (!description.TryGetMethodInfo(out MethodInfo methodInfo))
                {
                    return false;
                }

                IEnumerable<ApiVersion> apiVersions = methodInfo.DeclaringType!
                                                                .GetCustomAttributes(true)
                                                                .OfType<ApiVersionAttribute>()
                                                                .SelectMany(x => x.Versions);

                IEnumerable<ApiVersion> mapsToApiVersions = methodInfo.GetCustomAttributes(true)
                                                                      .OfType<MapToApiVersionAttribute>()
                                                                      .SelectMany(x => x.Versions);

                if (!mapsToApiVersions.Any())
                {
                    mapsToApiVersions = apiVersions.ToArray();
                }

                version = version.Replace("v", "");
                return apiVersions.Any(x => x.ToString() == version && mapsToApiVersions.Any(y => y.ToString() == version));
            });
        });

        return services;
    }

    /// <summary>
    /// Configures Swagger UI and launches Swagger UI in browser using configured path.
    /// </summary>
    /// <param name="webApplication">The web application used to configure the HTTP pipeline, and routes.</param>
    /// <returns>Reference to <see cref="webApplication"/></returns>
    public static WebApplication UseSwaggerInterface(this WebApplication webApplication)
    {
        webApplication.UseSwagger(options => options.RouteTemplate = SwaggerInfo.RouteTemplate);
        webApplication.UseSwaggerUI(options =>
        {
            ApiVersioningInfo.AvailableVersions.Reverse()
                                               .ToList()
                                               .ForEach(version => options.SwaggerEndpoint(SwaggerInfo.EndpointUrl(version), SwaggerInfo.EndpointName(version)));

            options.DocumentTitle = SwaggerInfo.DocumentTitle;
            options.RoutePrefix = SwaggerInfo.RoutePrefix;
            options.DocExpansion(DocExpansion.List);
        });

        return webApplication;
    }
}