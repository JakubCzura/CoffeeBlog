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
        await _userRepository.CreateAsync(user, cancellationToken);
        CreateUserViewModel result = _mapper.Map<CreateUserViewModel>(request);
        return result;
    }
}