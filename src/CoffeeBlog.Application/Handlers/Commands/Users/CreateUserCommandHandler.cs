using AutoMapper;
using CoffeeBlog.Application.Interfaces.Persistence.Repositories;
using CoffeeBlog.Domain.Commands.Users;
using CoffeeBlog.Domain.Entities;
using CoffeeBlog.Domain.ViewModels.Users;
using MediatR;

namespace CoffeeBlog.Application.Handlers.Commands.Users;

public class CreateUserCommandHandler(IUserRepository userRepository,
                                      IMapper mapper) : IRequestHandler<CreateUserCommand, CreateUserViewModel>
{
    private readonly IUserRepository _userRepository = userRepository;
    private readonly IMapper _mapper = mapper;

    public async Task<CreateUserViewModel> Handle(CreateUserCommand request,
                                                  CancellationToken cancellationToken)
    {
        User user = _mapper.Map<User>(request);
        if (!await _userRepository.AreUsernameAndEmailUniqueAndDifferentAsync(request.Username, request.Email, cancellationToken))
        {
            throw new Exception("User with given username or e-mail already exists. Email must be different from username.");
        }

        await _userRepository.CreateAsync(user, cancellationToken);
        CreateUserViewModel result = _mapper.Map<CreateUserViewModel>(request);
        return result;
    }
}