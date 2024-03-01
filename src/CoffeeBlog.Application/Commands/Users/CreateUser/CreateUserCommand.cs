using CoffeeBlog.Application.ViewModels.Users;
using MediatR;

namespace CoffeeBlog.Application.Commands.Users.CreateUser;

public class CreateUserCommand : IRequest<CreateUserViewModel>
{
    public string Username { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string ConfirmPassword { get; set; } = string.Empty;
}