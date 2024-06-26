﻿using AuthService.Application.Dtos.Accounts.Repository;
using AuthService.Application.Interfaces.Persistence.Repositories;
using AuthService.Domain.Errors.Users;
using AuthService.Domain.Resources;
using AuthService.Domain.ViewModels.Basics;
using AutoMapper;
using FluentResults;
using MediatR;

namespace AuthService.Application.Commands.Accounts.BanAccountByUserId;

/// <summary>
/// Command handler to ban user's account. It's related to <see cref="BanAccountByUserIdCommand"/>.
/// </summary>
/// <param name="_accountRepository">Interface to perform account operations in database.</param>
/// <param name="_userRepository">Interface to perform user operations in database.</param>
/// <param name="_mapper">AutoMapper to map classes.</param>
public class BanAccountByUserIdCommandHandler(IAccountRepository _accountRepository,
                                              IUserRepository _userRepository,
                                              IMapper _mapper)
    : IRequestHandler<BanAccountByUserIdCommand, Result<ViewModelBase>>
{
    private readonly IAccountRepository _accountRepository = _accountRepository;
    private readonly IUserRepository _userRepository = _userRepository;
    private readonly IMapper _mapper = _mapper;

    /// <summary>
    /// Handles request to ban user's account.
    /// </summary>
    /// <param name="request">Request command with details to ban user's account.</param>
    /// <param name="cancellationToken">Token to cancel asynchronous operation.</param>
    /// <returns><see cref="ViewModelBase"/></returns>
    public async Task<Result<ViewModelBase>> Handle(BanAccountByUserIdCommand request,
                                                    CancellationToken cancellationToken)
    {
        if (!await _userRepository.UserExistsAsync(request.UserId, cancellationToken))
        {
            return Result.Fail<ViewModelBase>(new UserNotFoundError());
        }

        BanAccountByUserIdDto banAccountByUserIdDto = _mapper.Map<BanAccountByUserIdDto>(request);
        await _accountRepository.BanAccountByUserIdAsync(banAccountByUserIdDto, cancellationToken);

        ViewModelBase result = new(ResponseMessages.AccountHasBeenBanned);
        return Result.Ok(result);
    }
}