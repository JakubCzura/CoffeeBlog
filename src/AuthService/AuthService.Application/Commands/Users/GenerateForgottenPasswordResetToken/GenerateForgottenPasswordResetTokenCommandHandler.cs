using AuthService.Application.Dtos.Users.Repository;
using AuthService.Application.Interfaces.Persistence.Repositories;
using AuthService.Application.Interfaces.Security.Token;
using AuthService.Domain.Entities;
using AuthService.Domain.Errors.Users;
using AuthService.Domain.Models.Security;
using EventBus.Application.Interfaces.Publishers;
using EventBus.Domain.Events.AuthService.Users;
using FluentResults;
using MediatR;
using Shared.Application.AuthService.Commands.Users.GenerateForgottenPasswordResetToken;
using Shared.Application.Common.Responses.Basics;
using Shared.Domain.AuthService.Resources;

namespace AuthService.Application.Commands.Users.GenerateForgottenPasswordResetToken;

/// <summary>
/// Command handler to generate password reset token for user who has forgotten password. It's related to <see cref="GenerateForgottenPasswordResetTokenCommand"/>.
/// </summary>
/// <param name="userRepository">Interface to perform user operations in database.</param>
/// <param name="securityTokenGenerator">Interface to generate security token to ensure user's identity.</param>
/// <param name="eventPublisher">Microservice to send event about generated token to reset password.</param>
public class GenerateForgottenPasswordResetTokenCommandHandler(IUserRepository userRepository,
                                                               ISecurityTokenGenerator securityTokenGenerator,
                                                               IEventPublisher eventPublisher)
    : IRequestHandler<GenerateForgottenPasswordResetTokenCommand, Result<ResponseBase>>
{
    /// <summary>
    /// Handles request to generate password reset token for user who has forgotten password.
    /// </summary>
    /// <param name="request">Request command with details to generate password reset token.</param>
    /// <param name="cancellationToken">Token to cancel asynchronous operation.</param>
    /// <returns>Instance of <see cref="ResponseBase"/></returns>
    public async Task<Result<ResponseBase>> Handle(GenerateForgottenPasswordResetTokenCommand request,
                                                   CancellationToken cancellationToken)
    {
        User? user = await userRepository.GetByEmailOrUsernameAsync(request.Email, cancellationToken);
        if (user is null)
        {
            return Result.Fail<ResponseBase>(new UserNotFoundError());
        }

        SecurityToken securityToken = securityTokenGenerator.GenerateForgottenPasswordResetToken();
        UpdateForgottenPasswordResetTokenDto updateForgottenPasswordResetTokenDto = new(request.Email, securityToken.Token, securityToken.ExpirationDate);
        await userRepository.UpdateForgottenPasswordResetTokenAsync(updateForgottenPasswordResetTokenDto, cancellationToken);

        PasswordResetTokenSentEvent passwordResetTokenSentEvent = new(request.Email,
                                                                      user.Username,
                                                                      updateForgottenPasswordResetTokenDto.ForgottenPasswordResetToken,
                                                                      updateForgottenPasswordResetTokenDto.ForgottenPasswordResetTokenExpiresAt,
                                                                      nameof(GenerateForgottenPasswordResetTokenCommandHandler));
        await eventPublisher.PublishAsync(passwordResetTokenSentEvent, cancellationToken);

        ResponseBase result = new(ResponseMessages.TokenToResetPasswordHasBeenSentToYourEmail);
        return Result.Ok(result);
    }
}