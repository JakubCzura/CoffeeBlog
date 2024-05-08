using AuthService.Domain.Enums;

namespace AuthService.Application.Dtos.Accounts;

/// <summary>
/// Request command to ban user's account. It's handled using Mediatr and CQRS pattern.
/// </summary>
/// <param name="UserId"> User's ID whose account will be banned. </param>
/// <param name="BanReason">Why user's account is banned. </param>
/// <param name="BanNote"> Note with details about ban. </param>
/// <param name="BanEndsAt"> Date and time of ban expiration. </param>
public record BanAccountByUserIdDto(int UserId,
                                    AccountBanReason? BanReason,
                                    string? BanNote,
                                    DateTime? BanEndsAt);