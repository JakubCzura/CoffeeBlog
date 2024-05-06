using AuthService.Domain.ViewModels.Basics;
using FluentResults;
using MediatR;

namespace AuthService.Application.Commands.Users.ChangeEmail;

/// <summary>
/// Request command to change user's e-mail. It's handled using Mediatr and CQRS pattern.
/// </summary>
public class ChangeEmailCommand : IRequest<Result<ViewModelBase>>
{
    ///<summary>
    /// User's new e-mail.
    /// </summary>
    public string NewEmail { get; set; } = string.Empty;
}