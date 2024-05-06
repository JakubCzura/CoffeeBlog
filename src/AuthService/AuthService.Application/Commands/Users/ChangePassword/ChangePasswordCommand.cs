using AuthService.Domain.ViewModels.Basics;
using FluentResults;
using MediatR;

namespace AuthService.Application.Commands.Users.ChangePassword;

/// <summary>
/// Request command to change user's password. It's handled using Mediatr and CQRS pattern.
/// </summary>
public class ChangePasswordCommand : IRequest<Result<ViewModelBase>>
{
    /// <summary>
    /// User's new password. It's going to be hashed when saving to database.
    /// </summary>
    public string NewPassword { get; set; } = string.Empty;

    /// <summary>
    /// It must match <see cref="NewPassword"/>.
    /// </summary>
    public string ConfirmNewPassword { get; set; } = string.Empty;
}