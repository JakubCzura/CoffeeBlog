using CoffeeBlog.Application.Interfaces.Persistence.Repositories;
using CoffeeBlog.Application.Interfaces.Security.Authentication;
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
/// <param name="_jwtService">Interface to create JWT token.</param>
public class SignInUserQueryHandler(IUserRepository _userRepository,
                                    IJwtService _jwtService) : IRequestHandler<SignInUserQuery, Result<SignInUserViewModel>>
{
    private readonly IUserRepository _userRepository = _userRepository;
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
        if (user == null)
        {
            return Result.Fail<SignInUserViewModel>(new UserNotFoundError());
        }

        string jwtToken = _jwtService.CreateToken(new(user.Id, user.Username, user.Email));

        SignInUserViewModel result = new() { JwtToken = jwtToken };
        return Result.Ok(result);
    }
}