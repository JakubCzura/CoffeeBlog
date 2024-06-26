﻿using AuthService.Application.ExtensionMethods.Automapper.Users;
using AuthService.Application.Interfaces.Persistence.Repositories;
using AuthService.Application.Interfaces.Security.Authentication;
using AuthService.Application.Interfaces.Security.Password;
using AuthService.Domain.Entities;
using AuthService.Domain.Errors.Users;
using AuthService.Domain.ViewModels.Users;
using AutoMapper;
using EventBus.Application.Interfaces.Publishers;
using EventBus.Domain.Events.AuthService.Users;
using FluentResults;
using MediatR;

namespace AuthService.Application.Commands.Users.SignUpUser;

/// <summary>
/// Command handler to sign up a new user and save this user to database. It's related to <see cref="SignUpUserCommand"/>.
/// </summary>
/// <param name="_userRepository">Interface to perform user operations in database.</param>
/// <param name="_userLastPasswordRepository">Interface to perform user's last passwords operations in database.</param>
/// <param name="_roleRepository">Interface to perform authorization roles operations in database.</param>
/// <param name="_userDetailRepository">Interface to perform user's details operations in database.</param>
/// <param name="_accountRepository">Interface to perform user's account's operations in database.</param>
/// <param name="_jwtService">Interface to create JWT token.</param>
/// <param name="_passwordHasher">Interface to hash password.</param>
/// <param name="_eventPublisher">Microservice to send event about user signing up.</param>
/// <param name="_mapper">AutoMapper to map classes.</param>
public class SignUpUserCommandHandler(IUserRepository _userRepository,
                                      IUserLastPasswordRepository _userLastPasswordRepository,
                                      IRoleRepository _roleRepository,
                                      IUserDetailRepository _userDetailRepository,
                                      IAccountRepository _accountRepository,
                                      IJwtService _jwtService,
                                      IPasswordHasher _passwordHasher,
                                      IEventPublisher _eventPublisher,
                                      IMapper _mapper)
    : IRequestHandler<SignUpUserCommand, Result<SignUpUserViewModel>>
{
    private readonly IUserRepository _userRepository = _userRepository;
    private readonly IUserLastPasswordRepository _userLastPasswordRepository = _userLastPasswordRepository;
    private readonly IRoleRepository _roleRepository = _roleRepository;
    private readonly IUserDetailRepository _userDetailRepository = _userDetailRepository;
    private readonly IAccountRepository _accountRepository = _accountRepository;
    private readonly IJwtService _jwtService = _jwtService;
    private readonly IPasswordHasher _passwordHasher = _passwordHasher;
    private readonly IEventPublisher _eventPublisher = _eventPublisher;
    private readonly IMapper _mapper = _mapper;

    /// <summary>
    /// Handles request to sign up a new user and add this user to database.
    /// </summary>
    /// <param name="request">Request command with details to sign up a new user.</param>
    /// <param name="cancellationToken">Token to cancel asynchronous operation.</param>
    /// <returns>Instance of <see cref="SignUpUserViewModel"/></returns>
    public async Task<Result<SignUpUserViewModel>> Handle(SignUpUserCommand request,
                                                          CancellationToken cancellationToken)
    {
        if (await _userRepository.UsernameExistsAsync(request.Username, cancellationToken))
        {
            return Result.Fail<SignUpUserViewModel>(new UsernameExistsError());
        }
        if (await _userRepository.EmailExistsAsync(request.Email, cancellationToken))
        {
            return Result.Fail<SignUpUserViewModel>(new EmailExistsError());
        }

        User user = _mapper.Map<User>(request, _passwordHasher.HashPassword(request.Password));
        await _userRepository.CreateAsync(user, cancellationToken);
        await _userDetailRepository.CreateAsync(new(user.Id), cancellationToken);
        await _accountRepository.CreateAsync(new(user.Id), cancellationToken);

        List<string> userRolesNames = await _roleRepository.GetAllRolesNamesByUserId(user.Id, cancellationToken);
        string jwtToken = _jwtService.CreateToken(new(user.Id, request.Username, request.Email), userRolesNames);

        await _eventPublisher.PublishAsync(new UserSignedUpEvent(user.Username, user.Email, nameof(SignUpUserCommandHandler)), cancellationToken);

        SignUpUserViewModel result = _mapper.Map<SignUpUserViewModel>(user, jwtToken);
        return Result.Ok(result);
    }
}