﻿using AuthService.Application.Interfaces.Persistence.Repositories;
using AuthService.Application.Interfaces.Security.Authentication;
using AuthService.Application.Interfaces.Security.Password;
using AuthService.Domain.Entities;
using AuthService.Domain.Errors.Users;
using AuthService.Domain.ViewModels.Users;
using FluentResults;
using MediatR;

namespace AuthService.Application.Queries.Users.SignInUser;

/// <summary>
/// Query handler to sign in a user. It's related to <see cref="SignInUserQuery"/>.
/// </summary>
/// <param name="_userRepository">Interface to perform user's operations in database.</param>
/// <param name="_roleRepository">Interface to perform authorization roles' operations in database.</param>
/// <param name="_userDetailRepository">Interface to perform user's details operations in database.</param>
/// <param name="_accountRepository">Interface to perform user's acount's operations in database.</param>
/// <param name="_passwordHasher">Interface to verify password.</param>
/// <param name="_jwtService">Interface to create JWT token.</param>
public class SignInUserQueryHandler(IUserRepository _userRepository,
                                    IRoleRepository _roleRepository,
                                    IUserDetailRepository _userDetailRepository,
                                    IAccountRepository _accountRepository,
                                    IPasswordHasher _passwordHasher,
                                    IJwtService _jwtService)
    : IRequestHandler<SignInUserQuery, Result<SignInUserViewModel>>
{
    private readonly IUserRepository _userRepository = _userRepository;
    private readonly IRoleRepository _roleRepository = _roleRepository;
    private readonly IUserDetailRepository _userDetailRepository = _userDetailRepository;
    private readonly IAccountRepository _accountRepository = _accountRepository;
    private readonly IPasswordHasher _passwordHasher = _passwordHasher;
    private readonly IJwtService _jwtService = _jwtService;

    /// <summary>
    /// Handles request to sign in a user.
    /// </summary>
    /// <param name="request">Request query with details to sign in a user.</param>
    /// <param name="cancellationToken">Token to cancel asynchronous operation.</param>
    /// <returns>Instance of <see cref="SignInUserViewModel"/></returns>
    public async Task<Result<SignInUserViewModel>> Handle(SignInUserQuery request,
                                                          CancellationToken cancellationToken)
    {
        User? user = await _userRepository.GetByEmailOrUsernameAsync(request.UsernameOrNickname, cancellationToken);

        //Don't reveal whether user was not found or password was incorrect due to potential security risks.
        //Just say that user was not found.
        //If statements are splitted to log failed sign in attempts if user was found but password was incorrect.
        if (user is null)
        {
            return Result.Fail<SignInUserViewModel>(new UserNotFoundError());
        }
        if (!_passwordHasher.VerifyPassword(request.Password, user.Password))
        {
            await _userDetailRepository.UpdateLastFailedSignInAsync(user.Id, cancellationToken);
            return Result.Fail<SignInUserViewModel>(new UserNotFoundError());
        }

        Account? account = await _accountRepository.GetAsync(user.Id, cancellationToken);
        if (account is not null && account.IsBanned)
        {
            await _userDetailRepository.UpdateLastFailedSignInAsync(user.Id, cancellationToken);
            return Result.Fail<SignInUserViewModel>(new UserBannedError(account.BanNote));
        }

        await _userDetailRepository.UpdateLastSuccessfullSignInAsync(user.Id, cancellationToken);

        List<string> userRolesNames = await _roleRepository.GetAllRolesNamesByUserId(user.Id, cancellationToken);
        string jwtToken = _jwtService.CreateToken(new(user.Id, user.Username, user.Email), userRolesNames);

        SignInUserViewModel result = new() { JwtToken = jwtToken };
        return Result.Ok(result);
    }
}