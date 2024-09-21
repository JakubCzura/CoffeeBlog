using FluentResults;
using Shared.Domain.AuthService.Resources;

namespace AuthService.Domain.Errors.Users;

/// <summary>
/// Error that occurs when user is banned and must not sign in.
/// </summary>
/// <param name="banReason">The reason why user's account has been banned.</param>
public class UserBannedError(string? banReason) : Error($"{ErrorMessages.ThisAccountHasBeenBanned}.{(!string.IsNullOrWhiteSpace(banReason) ? $" Reason: {banReason}" : "")}")
{
}