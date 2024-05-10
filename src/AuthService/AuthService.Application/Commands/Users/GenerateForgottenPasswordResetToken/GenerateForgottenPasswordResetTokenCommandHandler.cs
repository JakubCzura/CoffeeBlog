using AuthService.Application.Dtos.Users.Repository;
using AuthService.Application.Interfaces.Persistence.Repositories;
using AuthService.Application.Interfaces.Security.Token;
using AuthService.Domain.Entities;
using AuthService.Domain.Errors.Users;
using AuthService.Domain.Models.Security;
using AuthService.Domain.Resources;
using AuthService.Domain.ViewModels.Basics;
using EventBus.Application.Interfaces.Publishers;
using EventBus.Domain.Events.AuthService.Users;
using FluentResults;
using MediatR;

namespace AuthService.Application.Commands.Users.GenerateForgottenPasswordResetToken;

/// <summary>
/// Command handler to generate password reset token for user who has forgotten password. It's related to <see cref="GenerateForgottenPasswordResetTokenCommand"/>.
/// </summary>
/// <param name="_userRepository">Interface to perform user operations in database.</param>
/// <param name="_securityTokenGenerator">Interface to generate security token to ensure user's identity.</param>
/// <param name="_eventPublisher">Microservice to send event about generated token to reset password.</param>
public class GenerateForgottenPasswordResetTokenCommandHandler(IUserRepository _userRepository,
                                                               ISecurityTokenGenerator _securityTokenGenerator,
                                                               IEventPublisher _eventPublisher)
    : IRequestHandler<GenerateForgottenPasswordResetTokenCommand, Result<ViewModelBase>>
{
    private readonly IUserRepository _userRepository = _userRepository;
    private readonly ISecurityTokenGenerator _securityTokenGenerator = _securityTokenGenerator;
    private readonly IEventPublisher _eventPublisher = _eventPublisher;

    /// <summary>
    /// Handles request to generate password reset token for user who has forgotten password.
    /// </summary>
    /// <param name="request">Request command with details to generate password reset token.</param>
    /// <param name="cancellationToken">Token to cancel asynchronous operation.</param>
    /// <returns>Instance of <see cref="ViewModelBase"/></returns>
    public async Task<Result<ViewModelBase>> Handle(GenerateForgottenPasswordResetTokenCommand request,
                                                    CancellationToken cancellationToken)
    {
        User? user = await _userRepository.GetByEmailOrUsernameAsync(request.Email, cancellationToken);
        if (user is null)
        {
            return Result.Fail<ViewModelBase>(new UserNotFoundError());
        }

        SecurityToken securityToken = _securityTokenGenerator.GenerateForgottenPasswordResetToken();
        UpdateForgottenPasswordResetTokenDto updateForgottenPasswordResetTokenDto = new(request.Email, securityToken.Token, securityToken.ExpirationDate);
        await _userRepository.UpdateForgottenPasswordResetTokenAsync(updateForgottenPasswordResetTokenDto, cancellationToken);

        PasswordResetTokenSentEvent passwordResetTokenSentEvent = new(request.Email,
                                                                      user.Username,
                                                                      updateForgottenPasswordResetTokenDto.ForgottenPasswordResetToken,
                                                                      updateForgottenPasswordResetTokenDto.ForgottenPasswordResetTokenExpiresAt,
                                                                      nameof(GenerateForgottenPasswordResetTokenCommandHandler));
        await _eventPublisher.PublishAsync(passwordResetTokenSentEvent, cancellationToken);

        ViewModelBase result = new(ResponseMessages.TokenToResetPasswordHasBeenSentToYourEmail);
        return Result.Ok(result);
    }
}