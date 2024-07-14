using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;

namespace AuthService.Infrastructure.Security.Authentication;

/// <summary>
/// Configuration of JWT authentication.
/// </summary>
internal sealed class JwtAuthenticationConfiguration : IConfigureNamedOptions<AuthenticationOptions>
{
    /// <summary>
    /// Configures JWT authentication.
    /// </summary>
    /// <param name="name">The name of the options instance being configured.</param>
    /// <param name="options">The options instance to configure.</param>
    public void Configure(string? name, AuthenticationOptions options)
        => Configure(options);

    /// <summary>
    /// Configures JWT authentication.
    /// </summary>
    /// <param name="options">The options instance to configure.</param>
    public void Configure(AuthenticationOptions options)
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    }
}