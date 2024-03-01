using CoffeeBlog.Application.ViewModels.Users;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace CoffeeBlog.Application.Queries.Users.LogInUser;

public class LogInUserQuery : IRequest<LogInUserViewModel>
{
    /// <summary>
    /// User can log in with either username or email.
    /// Username and email are always unique.
    /// </summary>
    [Required(ErrorMessage = "Username or e-mail is required.")]
    [MaxLength(320)]
    public string UsernameOrNickname { get; set; } = string.Empty;

    /// <summary>
    /// User's password.
    /// </summary>
    [Required(ErrorMessage = "Password is required.")]
    [Length(5, 50, ErrorMessage = "Password must be between 5 and 50 characters long")]
    public string Password { get; set; } = string.Empty;
}