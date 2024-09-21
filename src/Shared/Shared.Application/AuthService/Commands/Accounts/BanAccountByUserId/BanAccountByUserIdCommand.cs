using FluentResults;
using MediatR;
using Shared.Application.Common.Responses.Basics;
using Shared.Domain.AuthService.Enums;

namespace Shared.Application.AuthService.Commands.Accounts.BanAccountByUserId;

/// <summary>
/// Request command to ban user's account. It's handled using Mediatr and CQRS pattern.
/// </summary>
/// <param name="UserId"> User's Id whose account will be banned. </param>
/// <param name="BanReason">Why user's account is banned. </param>
/// <param name="BanNote"> Note with details about ban. </param>
/// <param name="BanEndsAt"> Date of ban expiration. </param>
public record BanAccountByUserIdCommand(int UserId,
                                        AccountBanReason BanReason,
                                        string BanNote,
                                        DateOnly BanEndsAt) : BanAccountByUserIdRequest(BanReason, BanNote, BanEndsAt), IRequest<Result<ResponseBase>>;