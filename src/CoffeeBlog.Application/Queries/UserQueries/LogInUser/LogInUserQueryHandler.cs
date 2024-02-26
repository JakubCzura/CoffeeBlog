using CoffeeBlog.Application.Interfaces.Persistence.Repositories;
using CoffeeBlog.Application.Interfaces.Security.Authentication;
using CoffeeBlog.Application.ViewModels.UserViewModels;
using CoffeeBlog.Domain.Entities;
using MediatR;

namespace CoffeeBlog.Application.Queries.UserQueries.LogInUser;

public class LogInUserQueryHandler(IUserRepository userRepository,
                                   IJwtService jwtService) : IRequestHandler<LogInUserQuery, LogInUserViewModel>
{
    private readonly IUserRepository _userRepository = userRepository;
    private readonly IJwtService _jwtService = jwtService;

    public async Task<LogInUserViewModel> Handle(LogInUserQuery request,
                                           CancellationToken cancellationToken)
    {
        User? user = await _userRepository.GetByEmailOrUsernameAsync(request.UsernameOrNickname, cancellationToken);
        if (user == null)
        {
            throw new Exception("User with given e-mail or username does not exist.");
        }

        throw new NotImplementedException();
    }
}