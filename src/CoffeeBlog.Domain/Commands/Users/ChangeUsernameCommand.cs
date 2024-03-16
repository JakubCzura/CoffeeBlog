using CoffeeBlog.Domain.ViewModels.Basics;
using FluentResults;
using MediatR;

namespace CoffeeBlog.Domain.Commands.Users;

/// <summary>
/// Request command to change user's nickname. It's handled using Mediatr and CQRS pattern.
/// </summary>
public class ChangeUsernameCommand : IRequest<Result<ViewModelBase>>
{
    /// <summary>
    /// User's new username.
    /// </summary>
    public string NewUsername { get; set; } = string.Empty;
}