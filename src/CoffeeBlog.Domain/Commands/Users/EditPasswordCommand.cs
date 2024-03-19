using CoffeeBlog.Domain.ViewModels.Basics;
using FluentResults;
using MediatR;

namespace CoffeeBlog.Domain.Commands.Users;

/// <summary>
/// Request command to edit user's password. It's handled using Mediatr and CQRS pattern.
/// </summary>
public class EditPasswordCommand : IRequest<Result<ViewModelBase>>
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