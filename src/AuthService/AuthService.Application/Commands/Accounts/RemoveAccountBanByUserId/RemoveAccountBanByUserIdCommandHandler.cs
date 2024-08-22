using AuthService.Application.Interfaces.Persistence.Repositories;
using AuthService.Domain.Errors.Users;
using AuthService.Domain.Resources;
using AuthService.Domain.ViewModels.Basics;
using FluentResults;
using MediatR;

namespace AuthService.Application.Commands.Accounts.RemoveAccountBanByUserId;

/// <summary>
/// Command handler to remove user's account ban. It's related to <see cref="RemoveAccountBanByUserIdCommand"/>.
/// </summary>
/// <param name="accountRepository">Interface to perform account operations in database.</param>
/// <param name="userRepository">Interface to perform user operations in database.</param>
public class RemoveAccountBanByUserIdCommandHandler(IAccountRepository accountRepository,
                                                    IUserRepository userRepository)
    : IRequestHandler<RemoveAccountBanByUserIdCommand, Result<ViewModelBase>>
{
    /// <summary>
    /// Handles request to remove user's account ban.
    /// </summary>
    /// <param name="request">Request command with details to remove user's account ban.</param>
    /// <param name="cancellationToken">Token to cancel asynchronous operation.</param>
    /// <returns><see cref="ViewModelBase"/></returns>
    public async Task<Result<ViewModelBase>> Handle(RemoveAccountBanByUserIdCommand request,
                                                    CancellationToken cancellationToken)
    {
        if (!await userRepository.UserExistsAsync(request.UserId, cancellationToken))
        {
            return Result.Fail<ViewModelBase>(new UserNotFoundError());
        }

        await accountRepository.RemoveAccountBanByUserIdAsync(request.UserId, cancellationToken);

        ViewModelBase result = new(ResponseMessages.AccountBanHasBeenRemoved);

        return Result.Ok(result);
    }
}