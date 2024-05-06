using AuthService.Domain.ViewModels.Basics;
using FluentResults;
using MediatR;

namespace AuthService.Application.Commands.Users.ChangeUsername;

/// <summary>
/// Request command to change user's username. It's handled using Mediatr and CQRS pattern.
/// </summary>
public class ChangeUsernameCommand : IRequest<Result<ViewModelBase>>
{
    /// <summary>
    /// User's new username.
    /// </summary>
    public string NewUsername { get; set; } = string.Empty;
}