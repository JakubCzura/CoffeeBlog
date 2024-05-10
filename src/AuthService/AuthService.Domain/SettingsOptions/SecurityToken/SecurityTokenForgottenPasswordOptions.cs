namespace AuthService.Domain.SettingsOptions.SecurityToken;

/// <summary>
/// Configuration options for forgotten password.
/// </summary>
public class SecurityTokenForgottenPasswordOptions
{
    /// <summary>
    /// Byte count to calculate strong token to reset password.
    /// </summary>
    public int ByteCount { get; set; }

    /// <summary>
    /// Lifetime of token in minutes.
    /// </summary>
    public int LifetimeMinutes { get; set; }
}