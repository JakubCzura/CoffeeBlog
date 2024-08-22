using AuthService.Application.Interfaces.Persistence.Repositories;
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
/// <param name="userRepository">Interface to perform user's operations in database.</param>
/// <param name="roleRepository">Interface to perform authorization roles' operations in database.</param>
/// <param name="userDetailRepository">Interface to perform user's details operations in database.</param>
/// <param name="accountRepository">Interface to perform user's acount's operations in database.</param>
/// <param name="passwordHasher">Interface to verify password.</param>
/// <param name="jwtService">Interface to create JWT token.</param>
public class SignInUserQueryHandler(IUserRepository userRepository,
                                    IRoleRepository roleRepository,
                                    IUserDetailRepository userDetailRepository,
                                    IAccountRepository accountRepository,
                                    IPasswordHasher passwordHasher,
                                    IJwtService jwtService)
    : IRequestHandler<SignInUserQuery, Result<SignInUserViewModel>>
{
    /// <summary>
    /// Handles request to sign in a user.
    /// </summary>
    /// <param name="request">Request query with details to sign in a user.</param>
    /// <param name="cancellationToken">Token to cancel asynchronous operation.</param>
    /// <returns>Instance of <see cref="SignInUserViewModel"/></returns>
    public async Task<Result<SignInUserViewModel>> Handle(SignInUserQuery request,
                                                          CancellationToken cancellationToken)
    {
        User? user = await userRepository.GetByEmailOrUsernameAsync(request.UsernameOrNickname, cancellationToken);

        //Don't reveal whether user was not found or password was incorrect due to potential security risks.
        //Just say that user was not found.
        //If statements are splitted to log failed sign in attempts if user was found but password was incorrect.
        if (user is null)
        {
            return Result.Fail<SignInUserViewModel>(new UserNotFoundError());
        }
        if (!passwordHasher.VerifyPassword(request.Password, user.Password))
        {
            await userDetailRepository.UpdateLastFailedSignInAsync(user.Id, cancellationToken);
            return Result.Fail<SignInUserViewModel>(new UserNotFoundError());
        }

        Account? account = await accountRepository.GetAsync(user.Id, cancellationToken);
        if (account is not null && account.IsBanned)
        {
            await userDetailRepository.UpdateLastFailedSignInAsync(user.Id, cancellationToken);
            return Result.Fail<SignInUserViewModel>(new UserBannedError(account.BanNote));
        }

        await userDetailRepository.UpdateLastSuccessfullSignInAsync(user.Id, cancellationToken);

        List<string> userRolesNames = await roleRepository.GetAllRolesNamesByUserId(user.Id, cancellationToken);
        string jwtToken = jwtService.CreateToken(new(user.Id, user.Username, user.Email), userRolesNames);

        SignInUserViewModel result = new() { JwtToken = jwtToken };
        return Result.Ok(result);
    }
}