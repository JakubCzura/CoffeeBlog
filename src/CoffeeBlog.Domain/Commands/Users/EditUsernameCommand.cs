using CoffeeBlog.Domain.ViewModels.Basics;
using FluentResults;
using MediatR;

namespace CoffeeBlog.Domain.Commands.Users;

/// <summary>
/// Request command to edit user's username. It's handled using Mediatr and CQRS pattern.
/// </summary>
public class EditUsernameCommand : IRequest<Result<ViewModelBase>>
{
    /// <summary>
    /// User's new username.
    /// </summary>
    public string NewUsername { get; set; } = string.Empty;
}