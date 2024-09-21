using Shared.Domain.AuthService.Enums;

namespace AuthService.Application.Dtos.Accounts.Repository;

/// <summary>
/// Details to ban user's account.
/// </summary>
/// <param name="UserId"> User's ID whose account will be banned. </param>
/// <param name="BanReason">Why user's account is banned. </param>
/// <param name="BanNote"> Note with details about ban. </param>
/// <param name="BanEndsAt"> Date of ban expiration. </param>
public record BanAccountByUserIdDto(int UserId,
                                    AccountBanReason BanReason,
                                    string BanNote,
                                    DateOnly? BanEndsAt);