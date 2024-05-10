namespace AuthService.Domain.Exceptions;

/// <summary>
/// Exception thrown when a security token is invalid.
/// </summary>
/// <param name="message">Exception's details.</param>
public class SecurityTokenException(string? message) : Exception(message)
{
}