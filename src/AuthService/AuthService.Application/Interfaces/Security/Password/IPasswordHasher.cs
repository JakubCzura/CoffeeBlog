namespace AuthService.Application.Interfaces.Security.Password;

/// <summary>
/// Interface for hashing and verifying passwords.
/// </summary>
public interface IPasswordHasher
{
    /// <summary>
    /// Hashes the given plain-text password.
    /// </summary>
    /// <param name="password">Plain-text password that will be hashed.</param>
    /// <returns>Hashed password.</returns>
    /// <exception cref="ArgumentNullException">When password is null.</exception>
    public string HashPassword(string password);

    /// <summary>
    /// Checks if the given plain-text password matches the given hashed password.
    /// </summary>
    /// <param name="password">Plain-text password.</param>
    /// <param name="passwordHash">Hashed password.</param>
    /// <returns>True if the given plain-text password matches hashed password, otherwise false.</returns>
    /// <exception cref="ArgumentNullException">When password or password hash is null.</exception>
    public bool VerifyPassword(string password,
                               string passwordHash);
}