using AutoMapper;
using CoffeeBlog.Application.ExtensionMethods.AutoMapper;
using CoffeeBlog.Application.Interfaces.Persistence.Repositories;
using CoffeeBlog.Application.Interfaces.Security.Authentication;
using CoffeeBlog.Domain.Commands.Users;
using CoffeeBlog.Domain.Entities;
using CoffeeBlog.Domain.ViewModels.Users;
using MediatR;

namespace CoffeeBlog.Application.Handlers.Commands.Users;

public class CreateUserCommandHandler(IUserRepository _userRepository,
                                      IJwtService _jwtService,
                                      IMapper _mapper) : IRequestHandler<CreateUserCommand, CreateUserViewModel>
{
    private readonly IUserRepository _userRepository = _userRepository;
    private readonly IJwtService _service = _jwtService;
    private readonly IMapper _mapper = _mapper;

    public async Task<CreateUserViewModel> Handle(CreateUserCommand request,
                                                  CancellationToken cancellationToken)
    {
        User user = _mapper.Map<User>(request);
        if (!await _userRepository.AreUsernameAndEmailUniqueAndDifferentAsync(request.Username, request.Email, cancellationToken))
        {
            throw new Exception("User with given username or e-mail already exists. Email must be different from username.");
        }

        int userId = await _userRepository.CreateAsync(user, cancellationToken);
        string jwtToken = _service.CreateToken(new(userId, request.Username, request.Email));

        CreateUserViewModel result = _mapper.Map<CreateUserViewModel>(user, jwtToken);

        return result;
    }
}