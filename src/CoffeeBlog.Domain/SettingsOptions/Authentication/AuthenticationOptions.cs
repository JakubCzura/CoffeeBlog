namespace CoffeeBlog.Domain.SettingsOptions.Authentication;

/// <summary>
/// Class to hold authentication options based on appsettings.json.
/// </summary>
public class AuthenticationOptions
{
    /// <summary>
    /// Configuration key in appsettings.json.
    /// </summary>
    public const string AppsettingsKey = "Authentication";

    /// <summary>
    /// Authentication options for JWT.
    /// </summary>
    public AuthenticationJwtOptions Jwt { get; set; } = new();
}