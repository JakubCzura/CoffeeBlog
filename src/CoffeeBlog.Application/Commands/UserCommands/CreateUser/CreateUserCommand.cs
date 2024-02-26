using CoffeeBlog.Application.ViewModels.UserViewModels;
using MediatR;

namespace CoffeeBlog.Application.Commands.UserCommands.CreateUser;

public class CreateUserCommand : IRequest<CreateUserViewModel>
{
    public string Username { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string ConfirmPassword { get; set; } = string.Empty;
}