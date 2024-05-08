using AuthService.Domain.Resources;
using FluentResults;

namespace AuthService.Domain.Errors.Users;

public class UserBannedError(string? banReason) : Error($"{ErrorMessages.ThisAccountHasBeenBanned}.{(!string.IsNullOrWhiteSpace(banReason) ? $" Reason: {banReason}" : "")}")
{
}