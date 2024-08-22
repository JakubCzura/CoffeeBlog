using AuthService.Application.Interfaces.Security.Token;
using AuthService.Domain.Exceptions;
using AuthService.Domain.Models.Security;
using AuthService.Domain.Resources;
using AuthService.Domain.SettingsOptions.SecurityToken;
using Microsoft.Extensions.Options;
using System.Security.Cryptography;

namespace AuthService.Infrastructure.Security.Token;

/// <summary>
/// Generator of security tokens.
/// </summary>
/// <param name="_securityTokenOptions"></param>
internal class SecurityTokenGenerator(IOptions<SecurityTokenOptions> _securityTokenOptions) : ISecurityTokenGenerator
{
    private readonly SecurityTokenOptions _securityTokenOptions = _securityTokenOptions.Value;

    private static string GenerateToken(int byteCount = 32)
        => byteCount >= 32
        ? Convert.ToHexString(RandomNumberGenerator.GetBytes(byteCount))
        : throw new SecurityTokenException(ExceptionMessages.TokenValueIsInvalid);

    private static DateTime GenerateExpirationDate(int lifetimeMinutes = 1)
        => lifetimeMinutes >= 1
        ? DateTime.UtcNow.AddDays(lifetimeMinutes)
        : throw new SecurityTokenException(ExceptionMessages.TokenHasExpired);

    public SecurityToken GenerateForgottenPasswordResetToken()
        => new(GenerateToken(_securityTokenOptions.ForgottenPassword.ByteCount), GenerateExpirationDate(_securityTokenOptions.ForgottenPassword.LifetimeMinutes));
}