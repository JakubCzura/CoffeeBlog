using AutoMapper;
using CoffeeBlog.Application.ExtensionMethods.AutoMapper;
using CoffeeBlog.Application.Interfaces.Helpers;
using CoffeeBlog.Application.Interfaces.Persistence.Repositories;
using CoffeeBlog.Application.Interfaces.Security.Authentication;
using CoffeeBlog.Application.Interfaces.Security.Password;
using CoffeeBlog.Domain.Commands.Users;
using CoffeeBlog.Domain.Entities;
using CoffeeBlog.Domain.Errors.Users;
using CoffeeBlog.Domain.ViewModels.Users;
using FluentResults;
using MediatR;

namespace CoffeeBlog.Application.Handlers.Commands.Users;

/// <summary>
/// Command handler for creating a new user.
/// </summary>
/// <param name="_userRepository">Interface to perform user's operations in database.</param>
/// <param name="_jwtService">Interface to create JWT token.</param>
/// <param name="_passwordHasher">Interface to hash password.</param>
/// <param name="_mapper">AutoMapper to map classes.</param>
/// <param name="_dateTimeProvider">Interface to provide date and time.</param>
public class CreateUserCommandHandler(IUserRepository _userRepository,
                                      IJwtService _jwtService,
                                      IPasswordHasher _passwordHasher,
                                      IMapper _mapper,
                                      IDateTimeProvider _dateTimeProvider) : IRequestHandler<CreateUserCommand, Result<CreateUserViewModel>>
{
    private readonly IUserRepository _userRepository = _userRepository;
    private readonly IJwtService _jwtService = _jwtService;
    private readonly IPasswordHasher _passwordHasher = _passwordHasher;
    private readonly IMapper _mapper = _mapper;
    private readonly IDateTimeProvider _dateTimeProvider = _dateTimeProvider;

    /// <summary>
    /// Handles request to create a new user.
    /// </summary>
    /// <param name="request">Request command with details to create a new user.</param>
    /// <param name="cancellationToken">Token to cancel asynchronous operation.</param>
    /// <returns>Instance of <see cref="CreateUserViewModel"/></returns>
    /// <exception cref="Exception">When username or e-mail already exists in database or username and e-mail are the same.</exception>
    public async Task<Result<CreateUserViewModel>> Handle(CreateUserCommand request,
                                                          CancellationToken cancellationToken)
    {
        if (await _userRepository.UsernameExistsAsync(request.Username, cancellationToken))
        {
            return Result.Fail<CreateUserViewModel>(new UsernameExistsError("Username already exists."));
        }
        if (await _userRepository.EmailExistsAsync(request.Email, cancellationToken))
        {
            return Result.Fail<CreateUserViewModel>(new EmailExistsError("E-mail already exists."));
        }

        User user = new()
        {
            Username = request.Username,
            Email = request.Email,
            Password = _passwordHasher.HashPassword(request.Password),
            CreatedAt = _dateTimeProvider.UtcNow
        };

        int userId = await _userRepository.CreateAsync(user, cancellationToken);
        string jwtToken = _jwtService.CreateToken(new(userId, request.Username, request.Email));

        CreateUserViewModel result = _mapper.Map<CreateUserViewModel>(user, jwtToken);

        return result;
    }
}