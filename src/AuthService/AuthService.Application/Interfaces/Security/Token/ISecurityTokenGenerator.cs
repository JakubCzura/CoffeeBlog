using AuthService.Domain.Models.Security;

namespace AuthService.Application.Interfaces.Security.Token;

/// <summary>
/// Interface for generating security tokens for operations that need additional security validation.
/// </summary>
public interface ISecurityTokenGenerator
{
    /// <summary>
    /// Generates a security token with its expiration date to reset forgotten password.
    /// </summary>
    /// <returns>Security token with its expiration date.</returns>
    public SecurityToken GenerateForgottenPasswordResetToken();
}