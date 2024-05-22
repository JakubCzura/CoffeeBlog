using AuthService.Domain.ViewModels.Basics;
using FluentResults;
using MediatR;

namespace AuthService.Application.Commands.Accounts.RemoveAccountBanByUserId;

/// <summary>
/// Request command to remove ban from user's account. It's handled using Mediatr and CQRS pattern.
/// </summary>
/// <param name="UserId"> User's Id whose account's ban will be removed. </param>
public record RemoveAccountBanByUserIdCommand(int UserId) : IRequest<Result<ViewModelBase>>;