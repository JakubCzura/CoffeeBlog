using CoffeeBlog.Application.Interfaces.Security.Password;
using CoffeeBlog.Domain.SettingsOptions.PasswordHasher;
using Microsoft.Extensions.Options;
using System.Security.Cryptography;

namespace CoffeeBlog.Infrastructure.Security.Password;

public class PasswordHasher(IOptions<PasswordHasherOptions> passwordHasherOptions) : IPasswordHasher
{
    private readonly PasswordHasherOptions _passwordHasherOptions = passwordHasherOptions.Value;

    public string HashPassword(string password)
    {
        byte[] salt = RandomNumberGenerator.GetBytes(_passwordHasherOptions.KeySize);
        byte[] hash = CreateHash(password, salt);

        return string.Join(_passwordHasherOptions.Delimiter, Convert.ToBase64String(salt), Convert.ToBase64String(hash));
    }

    public bool VerifyPassword(string password, string passwordHash)
    {
        string[] saltAndHash = passwordHash.Split(_passwordHasherOptions.Delimiter);

        byte[] salt = Convert.FromBase64String(saltAndHash[0]);
        byte[] hash = Convert.FromBase64String(saltAndHash[1]);

        byte[] hashToVerify = CreateHash(password,salt);

        return CryptographicOperations.FixedTimeEquals(hash, hashToVerify);
    }

    private byte[] CreateHash(string password, byte[] salt)
        => Rfc2898DeriveBytes.Pbkdf2(password,
                                     salt,
                                     _passwordHasherOptions.Iterations,
                                     _passwordHasherOptions.HashAlgorithmName,
                                     _passwordHasherOptions.KeySize);
}