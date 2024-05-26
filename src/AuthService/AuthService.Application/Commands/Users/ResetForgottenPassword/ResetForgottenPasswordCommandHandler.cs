using AuthService.Application.Interfaces.Helpers;
using AuthService.Application.Interfaces.Persistence.Repositories;
using AuthService.Application.Interfaces.Security.Password;
using AuthService.Domain.Entities;
using AuthService.Domain.Errors.Users;
using AuthService.Domain.Resources;
using AuthService.Domain.ViewModels.Basics;
using EventBus.Application.Interfaces.Publishers;
using EventBus.Domain.Events.AuthService.Users;
using FluentResults;
using MediatR;

namespace AuthService.Application.Commands.Users.ResetForgottenPassword;

/// <summary>
/// Command handler to reset forgotten password. It's related to <see cref="ResetForgottenPasswordCommand"/>.
/// </summary>
/// <param name="_userRepository">Interface to perform user operations in database.</param>
/// <param name="_passwordHasher">Interface to hash password.</param>
/// <param name="_eventPublisher">Microservice to send event about generated token to reset password.</param>
/// <param name="_dateTimeProvider">Interface to deliver date and time.</param>
public class ResetForgottenPasswordCommandHandler(IUserRepository _userRepository,
                                                  IPasswordHasher _passwordHasher,
                                                  IEventPublisher _eventPublisher,
                                                  IDateTimeProvider _dateTimeProvider)
    : IRequestHandler<ResetForgottenPasswordCommand, Result<ViewModelBase>>
{
    private readonly IUserRepository _userRepository = _userRepository;
    private readonly IPasswordHasher _passwordHasher = _passwordHasher;
    private readonly IEventPublisher _eventPublisher = _eventPublisher;
    private readonly IDateTimeProvider _dateTimeProvider = _dateTimeProvider;

    /// <summary>
    /// Handles request to reset forgotten password.
    /// </summary>
    /// <param name="request">Request command with details to reset forgotten password.</param>
    /// <param name="cancellationToken">Token to cancel asynchronous operation.</param>
    /// <returns>Instance of <see cref="ViewModelBase"/></returns>
    public async Task<Result<ViewModelBase>> Handle(ResetForgottenPasswordCommand request,
                                                    CancellationToken cancellationToken)
    {
        User? user = await _userRepository.GetByEmailOrUsernameAsync(request.Email, cancellationToken);
        if (user is null)
        {
            return Result.Fail<ViewModelBase>(new UserNotFoundError());
        }
        if (user.ForgottenPasswordResetToken is not null && user.ForgottenPasswordResetToken != request.ForgottenPasswordResetToken)
        {
            return Result.Fail<ViewModelBase>(new InvalidForgottenPasswordResetTokenError());
        }
        if (user.ForgottenPasswordResetTokenExpiresAt is not null && user.ForgottenPasswordResetTokenExpiresAt > _dateTimeProvider.UtcNow)
        {
            return Result.Fail<ViewModelBase>(new ExpiredForgottenPasswordResetTokenError());
        }

        string hashedPassword = _passwordHasher.HashPassword(request.NewPassword);
        await _userRepository.ResetForgottenPasswordAsync(user.Id, request.NewPassword, cancellationToken);

        PasswordResetedEvent passwordResetedEvent = new(request.Email,
                                                        user.Username,
                                                        nameof(ResetForgottenPasswordCommandHandler));
        await _eventPublisher.PublishAsync(passwordResetedEvent, cancellationToken);

        ViewModelBase result = new(ResponseMessages.PasswordHasBeenReseted);
        return Result.Ok(result);
    }
}