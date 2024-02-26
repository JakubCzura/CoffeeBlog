using CoffeeBlog.Application.ViewModels.UserViewModels;
using MediatR;

namespace CoffeeBlog.Application.Queries.UserQueries.LogInUser;

public class LogInUserQuery : IRequest<LogInUserViewModel>
{
    /// <summary>
    /// User can log in with either username or email.
    /// Username and email are always unique.
    /// </summary>
    public string UsernameOrNickname { get; set; } = string.Empty;

    /// <summary>
    /// User's password.
    /// </summary>
    public string Password { get; set; } = string.Empty;
}