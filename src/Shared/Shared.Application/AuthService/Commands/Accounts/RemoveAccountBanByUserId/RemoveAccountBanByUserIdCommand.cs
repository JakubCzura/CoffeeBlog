using FluentResults;
using MediatR;
using Shared.Application.Common.Responses.Basics;

namespace Shared.Application.AuthService.Commands.Accounts.RemoveAccountBanByUserId;

/// <summary>
/// Request command to remove ban from user's account. It's handled using Mediatr and CQRS pattern.
/// </summary>
/// <param name="UserId"> User's Id whose account's ban will be removed. </param>
public record RemoveAccountBanByUserIdCommand(int UserId) : IRequest<Result<ResponseBase>>;