using AuthService.Application.Interfaces.Persistence.Repositories;
using AuthService.Application.Interfaces.Security.CurrentUsers;
using AuthService.Domain.Errors.Users;
using AuthService.Domain.Exceptions;
using AuthService.Domain.Models.Users;
using FluentResults;
using MediatR;
using Shared.Application.AuthService.Commands.Users.ChangeEmail;
using Shared.Application.Common.Responses.Basics;
using Shared.Domain.AuthService.Resources;

namespace AuthService.Application.Commands.Users.ChangeEmail;

/// <summary>
/// Command handler to change user's e-mail. It's related to <see cref="ChangeEmailCommand"/>.
/// </summary>
/// <param name="userRepository">Interface to perform user operations in database.</param>
/// <param name="userDetailRepository">Interface to perform user's details operations in database.</param>
/// <param name="currentUserContext">Interface to get information about current signed in user.</param>
public class ChangeEmailCommandHandler(IUserRepository userRepository,
                                       IUserDetailRepository userDetailRepository,
                                       ICurrentUserContext currentUserContext)
    : IRequestHandler<ChangeEmailCommand, Result<ResponseBase>>
{
    /// <summary>
    /// Handles request to change user's e-mail.
    /// </summary>
    /// <param name="request">Request command with details to change user's e-mail.</param>
    /// <param name="cancellationToken">Token to cancel asynchronous operation.</param>
    /// <returns>Instance of <see cref="ResponseBase"/></returns>
    /// <exception cref="UserUnauthorizedException">When user is not authorized.</exception>
    public async Task<Result<ResponseBase>> Handle(ChangeEmailCommand request,
                                                   CancellationToken cancellationToken)
    {
        CurrentAuthorizedUser currentAuthorizedUser = currentUserContext.GetCurrentAuthorizedUser();

        if (await userRepository.EmailExistsAsync(request.NewEmail, cancellationToken))
        {
            return Result.Fail<ResponseBase>(new EmailExistsError());
        }

        await userRepository.UpdateEmailAsync(currentAuthorizedUser.Id, request.NewEmail, cancellationToken);
        await userDetailRepository.UpdateLastEmailChangeAsync(currentAuthorizedUser.Id, cancellationToken);

        ResponseBase result = new(ResponseMessages.EmailHasBeenChanged);
        return Result.Ok(result);
    }
}