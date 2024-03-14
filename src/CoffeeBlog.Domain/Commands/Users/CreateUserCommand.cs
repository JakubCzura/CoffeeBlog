using CoffeeBlog.Domain.ViewModels.Users;
using FluentResults;
using MediatR;

namespace CoffeeBlog.Domain.Commands.Users;

/// <summary>
/// Request command to create a new user. It's handled using Mediatr and CQRS pattern.
/// </summary>
public class CreateUserCommand : IRequest<Result<CreateUserViewModel>>
{
    /// <summary>
    /// User's username. It's unique in database.
    /// </summary>
    public string Username { get; set; } = string.Empty;

    /// <summary>
    /// User's e-mail. It's unique in database.
    /// </summary>
    public string Email { get; set; } = string.Empty;

    /// <summary>
    /// User's password that will be hashed.
    /// </summary>
    public string Password { get; set; } = string.Empty;

    /// <summary>
    /// Confirmation of user's password. It must match <see cref="Password"/>.
    /// </summary>
    public string ConfirmPassword { get; set; } = string.Empty;
}