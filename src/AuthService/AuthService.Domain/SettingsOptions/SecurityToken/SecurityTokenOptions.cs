namespace AuthService.Domain.SettingsOptions.SecurityToken;

/// <summary>
/// Configuration options for security token.
/// </summary>
public class SecurityTokenOptions
{
    /// <summary>
    /// Key for security token settings in appsettings.json.
    /// </summary>
    public const string AppsettingsKey = "SecurityToken";

    /// <summary>
    /// Configuration options for forgotten password.
    /// </summary>
    public SecurityTokenForgottenPasswordOptions ForgottenPassword { get; set; } = new();
}