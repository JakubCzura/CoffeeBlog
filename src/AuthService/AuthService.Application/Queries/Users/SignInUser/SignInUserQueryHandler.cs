using AuthService.Application.Interfaces.Persistence.Repositories;
using AuthService.Application.Interfaces.Security.Authentication;
using AuthService.Application.Interfaces.Security.Password;
using AuthService.Domain.Entities;
using AuthService.Domain.Errors.Users;
using FluentResults;
using MassTransit.Initializers;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Shared.Application.AuthService.Queries.Users.SignInUser;
using Shared.Application.AuthService.Responses.Users;

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
                                    UserManager<User> userManager,
                                    SignInManager<User> signInManager,
                                    IRoleRepository roleRepository,
                                    IUserDetailRepository userDetailRepository,
                                    IAccountRepository accountRepository,
                                    IPasswordHasher passwordHasher,
                                    IJwtService jwtService)
    : IRequestHandler<SignInUserQuery, Result<SignInUserResponse>>
{
    /// <summary>
    /// Handles request to sign in a user.
    /// </summary>
    /// <param name="request">Request query with details to sign in a user.</param>
    /// <param name="cancellationToken">Token to cancel asynchronous operation.</param>
    /// <returns>Instance of <see cref="SignInUserResponse"/></returns>
    public async Task<Result<SignInUserResponse>> Handle(SignInUserQuery request,
                                                         CancellationToken cancellationToken)
    {
        //User? user = await userManager.FindByEmailAsync(request.Email);
        //if (user is null)
        //{
        //    return Result.Fail<SignInUserResponse>(new InvalidCredentialsError()); //Message to return to frontend like "Invalid credentials"
        //}

        //SignInResult signInResult = await signInManager.PasswordSignInAsync(user, request.Password, false, false);
        //if (!signInResult.Succeeded)
        //{
        //    await userDetailRepository.UpdateLastFailedSignInAsync(user.Id, cancellationToken);
        //    return Result.Fail<SignInUserResponse>(new InvalidCredentialsError()); //Message to return to frontend like "Invalid credentials"
        //}

        //See userManager - lockout
        //Account? account = await accountRepository.GetAsync(user.Id, cancellationToken);
        //if (account is not null && account.IsBanned)
        //{
        //    await userDetailRepository.UpdateLastFailedSignInAsync(user.Id, cancellationToken);
        //    return Result.Fail<SignInUserResponse>(new UserBannedError(account.BanNote));
        //}

        //await userDetailRepository.UpdateLastSuccessfullSignInAsync(user.Id, cancellationToken);

        //IList<string> userRoles = await userManager.GetRolesAsync(user);

        //string jwtToken = jwtService.CreateToken(new(user.Id, user.UserName, user.Email), userRoles);

        //SignInUserResponse result = new() { JwtToken = jwtToken };
        //return Result.Ok(result);
        throw new NotImplementedException();    
    }
}