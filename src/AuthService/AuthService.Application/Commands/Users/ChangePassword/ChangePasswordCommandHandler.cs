using AuthService.Application.Interfaces.Persistence.Repositories;
using AuthService.Application.Interfaces.Security.CurrentUsers;
using AuthService.Application.Interfaces.Security.Password;
using AuthService.Domain.Errors.Users;
using AuthService.Domain.Exceptions;
using AuthService.Domain.Models.Users;
using FluentResults;
using MediatR;
using Shared.Application.AuthService.Commands.Users.ChangePassword;
using Shared.Application.Common.Responses.Basics;
using Shared.Domain.Common.Resources.Translations;

namespace AuthService.Application.Commands.Users.ChangePassword;

/// <summary>
/// Command handler to change user's password. It's related to <see cref="ChangePasswordCommand"/>.
/// </summary>
/// <param name="userRepository">Interface to perform user operations in database.</param>
/// <param name="userDetailRepository">Interface to perform user's details operations in database.</param>
/// <param name="userLastPasswordRepository">Interface to perform user's last passwords operations in database.</param>
/// <param name="currentUserContext">Interface to get information about current signed in user.</param>
/// <param name="passwordHasher">Interface hash user's password.</param>
public class ChangePasswordCommandHandler(IUserRepository userRepository,
                                         IUserDetailRepository userDetailRepository,
                                         IUserLastPasswordRepository userLastPasswordRepository,
                                         ICurrentUserContext currentUserContext,
                                         IPasswordHasher passwordHasher)
    : IRequestHandler<ChangePasswordCommand, Result<ResponseBase>>
{
    /// <summary>
    /// Handles request to change user's password.
    /// </summary>
    /// <param name="request">Request command with details to change user's password.</param>
    /// <param name="cancellationToken">Token to cancel asynchronous operation.</param>
    /// <returns>Instance of <see cref="ResponseBase"/></returns>
    /// <exception cref="UserUnauthorizedException">When user is not authorized.</exception>
    public async Task<Result<ResponseBase>> Handle(ChangePasswordCommand request,
                                                    CancellationToken cancellationToken)
    {
        CurrentAuthorizedUser currentAuthorizedUser = currentUserContext.GetCurrentAuthorizedUser();

        string hashedPassword = passwordHasher.HashPassword(request.NewPassword);

        IEnumerable<string> userLastPasswords = (await userLastPasswordRepository.GetLastPasswordsByUserIdAsync(currentAuthorizedUser.Id, cancellationToken))
                                                                                 .Select(userLastPassword => userLastPassword.LastPassword);

        if (userLastPasswords.Contains(hashedPassword))
        {
            return Result.Fail<ResponseBase>(new PasswordAlreadyUsedError());
        }

        await userRepository.UpdatePasswordAsync(currentAuthorizedUser.Id, hashedPassword, cancellationToken);
        await userLastPasswordRepository.CreateAsync(new(hashedPassword, currentAuthorizedUser.Id), cancellationToken);
        await userDetailRepository.UpdateLastPasswordChangeAsync(currentAuthorizedUser.Id, cancellationToken);

        ResponseBase result = new(ResponseMessages.PasswordHasBeenChanged);
        return Result.Ok(result);
    }
}