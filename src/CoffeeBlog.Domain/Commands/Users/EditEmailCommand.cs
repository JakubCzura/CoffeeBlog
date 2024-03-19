using CoffeeBlog.Domain.ViewModels.Basics;
using FluentResults;
using MediatR;

namespace CoffeeBlog.Domain.Commands.Users;

/// <summary>
/// Request command to edit user's e-mail. It's handled using Mediatr and CQRS pattern.
/// </summary>
public class EditEmailCommand : IRequest<Result<ViewModelBase>>
{
    ///<summary>
    /// User's new e-mail.
    /// </summary>
    public string NewEmail { get; set; } = string.Empty;
}