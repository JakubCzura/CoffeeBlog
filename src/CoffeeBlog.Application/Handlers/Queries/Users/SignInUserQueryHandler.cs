using CoffeeBlog.Application.Interfaces.Persistence.Repositories;
using CoffeeBlog.Application.Interfaces.Security.Authentication;
using CoffeeBlog.Application.Interfaces.Security.Password;
using CoffeeBlog.Domain.Entities;
using CoffeeBlog.Domain.Errors.Users;
using CoffeeBlog.Domain.Queries.Users;
using CoffeeBlog.Domain.ViewModels.Users;
using FluentResults;
using MediatR;

namespace CoffeeBlog.Application.Handlers.Queries.Users;

/// <summary>
/// Query handler for signing in a user.
/// </summary>
/// <param name="_userRepository">Interface to perform user's operations in database.</param>
/// <param name="_roleRepository">Interface to perform authorization roles' operations in database.</param>
/// <param name="_passwordHasher">Interface to verify password.</param>
/// <param name="_jwtService">Interface to create JWT token.</param>
public class SignInUserQueryHandler(IUserRepository _userRepository,
                                    IRoleRepository _roleRepository,
                                    IPasswordHasher _passwordHasher,
                                    IJwtService _jwtService) : IRequestHandler<SignInUserQuery, Result<SignInUserViewModel>>
{
    private readonly IUserRepository _userRepository = _userRepository;
    private readonly IRoleRepository _roleRepository = _roleRepository;
    private readonly IPasswordHasher _passwordHasher = _passwordHasher;
    private readonly IJwtService _jwtService = _jwtService;

    /// <summary>
    /// Handles request to signing in a user.
    /// </summary>
    /// <param name="request">Request query with details to sign in a user.</param>
    /// <param name="cancellationToken">Token to cancel asynchronous operation.</param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    /// <exception cref="NotImplementedException"></exception>
    public async Task<Result<SignInUserViewModel>> Handle(SignInUserQuery request,
                                                          CancellationToken cancellationToken)
    {
        User? user = await _userRepository.GetByEmailOrUsernameAsync(request.UsernameOrNickname, cancellationToken);

        //Don't reveal whether user was not found or password was incorrect due to potential security risks.
        //Just say that user was not found.
        if (user == null || !_passwordHasher.VerifyPassword(request.Password, user.Password))
        {
            return Result.Fail<SignInUserViewModel>(new UserNotFoundError());
        }

        IEnumerable<string> userRoles = (await _roleRepository.GetAllByUserId(user.Id, cancellationToken))
                                                              .Select(role => role.Name);

        string jwtToken = _jwtService.CreateToken(new(user.Id, user.Username, user.Email), userRoles);

        SignInUserViewModel result = new() { JwtToken = jwtToken };
        return Result.Ok(result);
    }
}