using Shared.Domain.AuthService.Enums;

namespace Shared.Application.AuthService.Commands.Accounts.BanAccountByUserId;

/// <summary>
/// Request to ban user's account.
/// </summary>
/// <param name="BanReason">Why user's account is banned.</param>
/// <param name="BanNote">Note with details about ban.</param>
/// <param name="BanEndsAt">Date of ban expiration.</param>
public record BanAccountByUserIdRequest(AccountBanReason BanReason,
                                        string BanNote,
                                        DateOnly BanEndsAt);