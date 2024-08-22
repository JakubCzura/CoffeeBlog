using AuthService.Application.Interfaces.Security.Password;
using AuthService.Domain.SettingsOptions.PasswordHasher;
using Microsoft.Extensions.Options;
using System.Security.Cryptography;

namespace AuthService.Infrastructure.Security.Password;

/// <summary>
/// Security service to hash and verify passwords. Passwords should be always securely stored.
/// </summary>
/// <param name="_passwordHasherOptions">Settings to perform hashing and verifying passwords.</param>
internal class PasswordHasher(IOptions<PasswordHasherOptions> _passwordHasherOptions) : IPasswordHasher
{
    private readonly PasswordHasherOptions _passwordHasherOptions = _passwordHasherOptions.Value;

    public string HashPassword(string password)
    {
        if (string.IsNullOrWhiteSpace(password))
        {
            throw new ArgumentNullException(nameof(password));
        }

        byte[] salt = RandomNumberGenerator.GetBytes(_passwordHasherOptions.KeySize);
        byte[] hash = CreateHash(password, salt);

        return string.Join(_passwordHasherOptions.Delimiter, Convert.ToBase64String(salt), Convert.ToBase64String(hash));
    }

    public bool VerifyPassword(string password,
                               string passwordHash)
    {
        if (string.IsNullOrWhiteSpace(password))
        {
            throw new ArgumentNullException(nameof(password));
        }

        if (string.IsNullOrWhiteSpace(passwordHash))
        {
            throw new ArgumentNullException(nameof(passwordHash));
        }

        string[] saltAndHash = passwordHash.Split(_passwordHasherOptions.Delimiter);

        byte[] salt = Convert.FromBase64String(saltAndHash[0]);
        byte[] hash = Convert.FromBase64String(saltAndHash[1]);

        byte[] hashToVerify = CreateHash(password, salt);

        return CryptographicOperations.FixedTimeEquals(hash, hashToVerify);
    }

    private byte[] CreateHash(string password,
                              byte[] salt)
        => Rfc2898DeriveBytes.Pbkdf2(password,
                                     salt,
                                     _passwordHasherOptions.Iterations,
                                     _passwordHasherOptions.HashAlgorithmName,
                                     _passwordHasherOptions.KeySize);
}