using AutoMapper;
using CoffeeBlog.Application.Interfaces.Persistence.Repositories;
using CoffeeBlog.Application.ViewModels.User;
using CoffeeBlog.Domain.Entities;
using MediatR;

namespace CoffeeBlog.Application.Commands.User.CreateUser;

public class CreateUserCommandHandler(IUserRepository userRepository,
                                      IMapper mapper) : IRequestHandler<CreateUserCommand, CreateUserViewModel>
{
    private readonly IUserRepository _userRepository = userRepository;
    private readonly IMapper _mapper = mapper;

    public async Task<CreateUserViewModel> Handle(CreateUserCommand request,
                                                  CancellationToken cancellationToken)
    {
        UserEntity user = _mapper.Map<UserEntity>(request);
        if(await _userRepository.AreUsernameAndEmailUniqueAndDifferentAsync(request.Email, request.Username, cancellationToken))
        {
            throw new Exception("User with given e-mail or username already exists. Email must be different from username.");
        }

        await _userRepository.CreateAsync(user, cancellationToken);
        CreateUserViewModel result = _mapper.Map<CreateUserViewModel>(request);
        return result;
    }
}