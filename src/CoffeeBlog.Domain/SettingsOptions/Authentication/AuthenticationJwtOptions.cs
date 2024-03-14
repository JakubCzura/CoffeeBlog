namespace CoffeeBlog.Domain.SettingsOptions.Authentication;

/// <summary>
/// Configuration options for JWT authentication based on appsettings.json.
/// </summary>
public class AuthenticationJwtOptions
{
    /// <summary>
    /// Secret key for JWT signature.
    /// </summary>
    public string SecretKey { get; set; } = string.Empty;

    /// <summary>
    /// JWT provider.
    /// </summary>
    public string Issuer { get; set; } = string.Empty;

    /// <summary>
    /// JWT recipient.
    /// </summary>
    public string Audience { get; set; } = string.Empty;

    /// <summary>
    /// How long token is valid.
    /// </summary>
    public int LifetimeInMinutes { get; set; }

    /// <summary>
    /// Determines if issuer will be validated.
    /// </summary>
    public bool ValidateIssuer { get; set; } = true;

    /// <summary>
    /// Determines if audience will be validated.
    /// </summary>
    public bool ValidateAudience { get; set; } = true;

    /// <summary>
    /// Determines if token's lifetime will be validated.
    /// </summary>
    public bool ValidateLifetime { get; set; } = true;

    /// <summary>
    /// Determines if token's signature will be validated.
    /// </summary>
    public bool ValidateIssuerSigningKey { get; set; } = true;
}