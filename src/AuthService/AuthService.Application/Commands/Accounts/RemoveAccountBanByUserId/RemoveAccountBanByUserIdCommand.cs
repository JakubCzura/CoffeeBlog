using AuthService.Domain.ViewModels.Basics;
using FluentResults;
using MediatR;

namespace AuthService.Application.Commands.Accounts.RemoveAccountBanByUserId;

public record RemoveAccountBanByUserIdCommand(int UserId) : IRequest<Result<ViewModelBase>>;