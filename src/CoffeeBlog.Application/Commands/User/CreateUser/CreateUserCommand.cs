using CoffeeBlog.Application.ViewModels.User;
using MediatR;

namespace CoffeeBlog.Application.Commands.User.CreateUser;

public class CreateUserCommand : IRequest<CreateUserViewModel>
{
    public string Username { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string ConfirmPassword { get; set; } = string.Empty;
}