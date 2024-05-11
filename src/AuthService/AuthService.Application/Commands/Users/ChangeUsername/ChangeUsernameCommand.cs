using AuthService.Domain.ViewModels.Basics;
using FluentResults;
using MediatR;

namespace AuthService.Application.Commands.Users.ChangeUsername;

/// <summary>
/// Request command to change user's username. It's handled using Mediatr and CQRS pattern.
/// </summary>
/// <param name="NewUsername"> User's new username. </param>
public record ChangeUsernameCommand(string NewUsername) : IRequest<Result<ViewModelBase>>;