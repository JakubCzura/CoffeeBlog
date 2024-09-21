using AuthService.Application.ExtensionMethods.Automapper.Users;
using AuthService.Application.Interfaces.Persistence.Repositories;
using AuthService.Application.Interfaces.Security.Authentication;
using AuthService.Application.Interfaces.Security.Password;
using AuthService.Domain.Entities;
using AuthService.Domain.Errors.Users;
using AutoMapper;
using EventBus.Application.Interfaces.Publishers;
using EventBus.Domain.Events.AuthService.Users;
using FluentResults;
using MediatR;
using Shared.Application.AuthService.Commands.Users.SignUpUser;
using Shared.Application.AuthService.Responses.Users;

namespace AuthService.Application.Commands.Users.SignUpUser;

/// <summary>
/// Command handler to sign up a new user and save this user to database. It's related to <see cref="SignUpUserCommand"/>.
/// </summary>
/// <param name="userRepository">Interface to perform user operations in database.</param>
/// <param name="roleRepository">Interface to perform authorization roles operations in database.</param>
/// <param name="userDetailRepository">Interface to perform user's details operations in database.</param>
/// <param name="accountRepository">Interface to perform user's account's operations in database.</param>
/// <param name="jwtService">Interface to create JWT token.</param>
/// <param name="passwordHasher">Interface to hash password.</param>
/// <param name="eventPublisher">Microservice to send event about user signing up.</param>
/// <param name="mapper">AutoMapper to map classes.</param>
public class SignUpUserCommandHandler(IUserRepository userRepository,
                                      IRoleRepository roleRepository,
                                      IUserDetailRepository userDetailRepository,
                                      IAccountRepository accountRepository,
                                      IJwtService jwtService,
                                      IPasswordHasher passwordHasher,
                                      IEventPublisher eventPublisher,
                                      IMapper mapper)
    : IRequestHandler<SignUpUserCommand, Result<SignUpUserResponse>>
{
    /// <summary>
    /// Handles request to sign up a new user and add this user to database.
    /// </summary>
    /// <param name="request">Request command with details to sign up a new user.</param>
    /// <param name="cancellationToken">Token to cancel asynchronous operation.</param>
    /// <returns>Instance of <see cref="SignUpUserResponse"/></returns>
    public async Task<Result<SignUpUserResponse>> Handle(SignUpUserCommand request,
                                                          CancellationToken cancellationToken)
    {
        if (await userRepository.UsernameExistsAsync(request.Username, cancellationToken))
        {
            return Result.Fail<SignUpUserResponse>(new UsernameExistsError());
        }
        if (await userRepository.EmailExistsAsync(request.Email, cancellationToken))
        {
            return Result.Fail<SignUpUserResponse>(new EmailExistsError());
        }

        User user = mapper.Map<User>(request, passwordHasher.HashPassword(request.Password));
        await userRepository.CreateAsync(user, cancellationToken);
        await userDetailRepository.CreateAsync(new(user.Id), cancellationToken);
        await accountRepository.CreateAsync(new(user.Id), cancellationToken);

        List<string> userRolesNames = await roleRepository.GetAllRolesNamesByUserId(user.Id, cancellationToken);
        string jwtToken = jwtService.CreateToken(new(user.Id, request.Username, request.Email), userRolesNames);

        await eventPublisher.PublishAsync(new UserSignedUpEvent(user.Username, user.Email, nameof(SignUpUserCommandHandler)), cancellationToken);

        SignUpUserResponse result = mapper.Map<SignUpUserResponse>(user, jwtToken);
        return Result.Ok(result);
    }
}