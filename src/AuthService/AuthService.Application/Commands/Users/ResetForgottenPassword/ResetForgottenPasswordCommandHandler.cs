﻿using AuthService.Application.Interfaces.Helpers;
using AuthService.Application.Interfaces.Persistence.Repositories;
using AuthService.Application.Interfaces.Security.Password;
using AuthService.Domain.Entities;
using AuthService.Domain.Errors.Users;
using EventBus.Application.Interfaces.Publishers;
using EventBus.Domain.Events.AuthService.Users;
using FluentResults;
using MediatR;
using Shared.Application.AuthService.Commands.Users.ResetForgottenPassword;
using Shared.Application.Common.Responses.Basics;
using Shared.Domain.Common.Resources.Translations;

namespace AuthService.Application.Commands.Users.ResetForgottenPassword;

/// <summary>
/// Command handler to reset forgotten password. It's related to <see cref="ResetForgottenPasswordCommand"/>.
/// </summary>
/// <param name="userRepository">Interface to perform user operations in database.</param>
/// <param name="passwordHasher">Interface to hash password.</param>
/// <param name="eventPublisher">Microservice to send event about generated token to reset password.</param>
/// <param name="dateTimeProvider">Interface to deliver date and time.</param>
public class ResetForgottenPasswordCommandHandler(IUserRepository userRepository,
                                                  IPasswordHasher passwordHasher,
                                                  IEventPublisher eventPublisher,
                                                  IDateTimeProvider dateTimeProvider)
    : IRequestHandler<ResetForgottenPasswordCommand, Result<ResponseBase>>
{
    /// <summary>
    /// Handles request to reset forgotten password.
    /// </summary>
    /// <param name="request">Request command with details to reset forgotten password.</param>
    /// <param name="cancellationToken">Token to cancel asynchronous operation.</param>
    /// <returns>Instance of <see cref="ResponseBase"/></returns>
    public async Task<Result<ResponseBase>> Handle(ResetForgottenPasswordCommand request,
                                                   CancellationToken cancellationToken)
    {
        User? user = await userRepository.GetByEmailOrUsernameAsync(request.Email, cancellationToken);
        if (user is null)
        {
            return Result.Fail<ResponseBase>(new UserNotFoundError());
        }
        if (user.ForgottenPasswordResetToken is not null && user.ForgottenPasswordResetToken != request.ForgottenPasswordResetToken)
        {
            return Result.Fail<ResponseBase>(new InvalidForgottenPasswordResetTokenError());
        }
        if (user.ForgottenPasswordResetTokenExpiresAt is not null && user.ForgottenPasswordResetTokenExpiresAt > dateTimeProvider.UtcNow)
        {
            return Result.Fail<ResponseBase>(new ExpiredForgottenPasswordResetTokenError());
        }

        string hashedPassword = passwordHasher.HashPassword(request.NewPassword);
        await userRepository.ResetForgottenPasswordAsync(user.Id, request.NewPassword, cancellationToken);

        PasswordResetedEvent passwordResetedEvent = new(request.Email,
                                                        user.Username,
                                                        nameof(ResetForgottenPasswordCommandHandler));
        await eventPublisher.PublishAsync(passwordResetedEvent, cancellationToken);

        ResponseBase result = new(ResponseMessages.PasswordHasBeenReseted);
        return Result.Ok(result);
    }
}