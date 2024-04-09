using System.Security.Cryptography;

namespace AuthService.Domain.SettingsOptions.PasswordHasher;

/// <summary>
/// Configuration options for password hashing.
/// </summary>
public class PasswordHasherOptions
{
    /// <summary>
    /// Key for password hasher settings in appsettings.json.
    /// </summary>
    public const string AppsettingsKey = "PasswordHasher";

    /// <summary>
    /// Delimiter between salt and hash.
    /// </summary>
    public char Delimiter { get; set; }

    /// <summary>
    /// Size of the security key.
    /// </summary>
    public int KeySize { get; set; }

    /// <summary>
    /// Iterations for password hashing.
    /// </summary>
    public int Iterations { get; set; }

    /// <summary>
    /// Algorithm for hashing.
    /// </summary>
    public HashAlgorithmName HashAlgorithmName { get; set; } = HashAlgorithmName.SHA512;
}