using System.Security.Cryptography;

namespace CoffeeBlog.Domain.SettingsOptions.PasswordHasher;

public class PasswordHasherOptions
{
    public const string AppsettingsKey = "PasswordHasher";
    public char Delimiter { get; set; }
    public int KeySize { get; set; }
    public int Iterations { get; set; }
    public HashAlgorithmName HashAlgorithmName { get; set; } = HashAlgorithmName.SHA512;
}