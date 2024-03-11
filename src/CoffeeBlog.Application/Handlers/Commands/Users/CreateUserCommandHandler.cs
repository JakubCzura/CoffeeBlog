using AutoMapper;
using CoffeeBlog.Application.ExtensionMethods.AutoMapper;
using CoffeeBlog.Application.Interfaces.Helpers;
using CoffeeBlog.Application.Interfaces.Persistence.Repositories;
using CoffeeBlog.Application.Interfaces.Security.Authentication;
using CoffeeBlog.Application.Interfaces.Security.Password;
using CoffeeBlog.Domain.Commands.Users;
using CoffeeBlog.Domain.Entities;
using CoffeeBlog.Domain.ViewModels.Users;
using MediatR;

namespace CoffeeBlog.Application.Handlers.Commands.Users;

public class CreateUserCommandHandler(IUserRepository _userRepository,
                                      IJwtService _jwtService,
                                      IPasswordHasher _passwordHasher,
                                      IMapper _mapper,
                                      IDateTimeProvider _dateTimeProvider) : IRequestHandler<CreateUserCommand, CreateUserViewModel>
{
    private readonly IUserRepository _userRepository = _userRepository;
    private readonly IJwtService _service = _jwtService;
    private readonly IPasswordHasher _passwordHasher = _passwordHasher;
    private readonly IMapper _mapper = _mapper;
    private readonly IDateTimeProvider _dateTimeProvider = _dateTimeProvider;

    public async Task<CreateUserViewModel> Handle(CreateUserCommand request,
                                                  CancellationToken cancellationToken)
    {
        User user = new()
        {
            Username = request.Username,
            Email = request.Email,
            Password = _passwordHasher.HashPassword(request.Password),
            CreatedAt = _dateTimeProvider.UtcNow            
        };

        if(request.Username == request.Email)
        {
            throw new Exception("Username and e-mail cannot be same.");
        }
        if (await _userRepository.UsernameExistsAsync(request.Username, cancellationToken))
        {
            throw new Exception("Username already exists.");
        }
        if (await _userRepository.EmailExistsAsync(request.Email, cancellationToken))
        {
            throw new Exception("E-mail already exists.");
        }

        int userId = await _userRepository.CreateAsync(user, cancellationToken);
        string jwtToken = _service.CreateToken(new(userId, request.Username, request.Email));

        CreateUserViewModel result = _mapper.Map<CreateUserViewModel>(user, jwtToken);

        return result;
    }
}