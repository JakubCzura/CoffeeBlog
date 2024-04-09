namespace AuthService.Domain.SettingsOptions.Authentication;

/// <summary>
/// Configuration options for authentication based on appsettings.json.
/// </summary>
public class AuthenticationOptions
{
    /// <summary>
    /// Key for authentication options in appsettings.json.
    /// </summary>
    public const string AppsettingsKey = "Authentication";

    /// <summary>
    /// Authentication options for JWT.
    /// </summary>
    public AuthenticationJwtOptions Jwt { get; set; } = new();
}