using AuthService.Application.Dtos.Accounts.Repository;
using AuthService.Application.Interfaces.Persistence.Repositories;
using AuthService.Domain.Errors.Users;
using AutoMapper;
using FluentResults;
using MediatR;
using Shared.Application.AuthService.Commands.Accounts.BanAccountByUserId;
using Shared.Application.Common.Responses.Basics;
using Shared.Domain.Common.Resources.Translations;

namespace AuthService.Application.Commands.Accounts.BanAccountByUserId;

/// <summary>
/// Command handler to ban user's account. It's related to <see cref="BanAccountByUserIdCommand"/>.
/// </summary>
/// <param name="accountRepository">Interface to perform account operations in database.</param>
/// <param name="userRepository">Interface to perform user operations in database.</param>
/// <param name="mapper">AutoMapper to map classes.</param>
public class BanAccountByUserIdCommandHandler(IAccountRepository accountRepository,
                                              IUserRepository userRepository,
                                              IMapper mapper)
    : IRequestHandler<BanAccountByUserIdCommand, Result<ResponseBase>>
{
    /// <summary>
    /// Handles request to ban user's account.
    /// </summary>
    /// <param name="request">Request command with details to ban user's account.</param>
    /// <param name="cancellationToken">Token to cancel asynchronous operation.</param>
    /// <returns><see cref="ResponseBase"/></returns>
    public async Task<Result<ResponseBase>> Handle(BanAccountByUserIdCommand request,
                                                   CancellationToken cancellationToken)
    {
        if (!await userRepository.UserExistsAsync(request.UserId, cancellationToken))
        {
            return Result.Fail<ResponseBase>(new UserNotFoundError());
        }

        BanAccountByUserIdDto banAccountByUserIdDto = mapper.Map<BanAccountByUserIdDto>(request);
        await accountRepository.BanAccountByUserIdAsync(banAccountByUserIdDto, cancellationToken);

        ResponseBase result = new(ResponseMessages.AccountHasBeenBanned);
        return Result.Ok(result);
    }
}