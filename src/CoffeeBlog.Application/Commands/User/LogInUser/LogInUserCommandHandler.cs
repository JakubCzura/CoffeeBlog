using CoffeeBlog.Application.Interfaces.Persistence.Repositories;
using CoffeeBlog.Application.Interfaces.Security.Authentication;
using CoffeeBlog.Application.ViewModels.User;
using MediatR;

namespace CoffeeBlog.Application.Commands.User.LogInUser;

public class LogInUserCommandHandler(IUserRepository userRepository,
                                     IJwtService jwtService) : IRequestHandler<LogInUserCommand, LogInUserViewModel>
{
    private readonly IUserRepository _userRepository = userRepository;
    private readonly IJwtService _jwtService = jwtService;

    public Task<LogInUserViewModel> Handle(LogInUserCommand request,
                                           CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}