namespace AuthService.Domain.Models.Security;

/// <summary>
/// Security token with its expiration date to provide security for operations like resetting password.
/// </summary>
/// <param name="Token">Security token that user should type and must match this token to provide user's identity.</param>
/// <param name="ExpirationDate">Expiration date of token.</param>
public record SecurityToken(string Token,
                            DateTime ExpirationDate);