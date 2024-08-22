using AuthService.Application.Interfaces.Persistence.Repositories;
using AuthService.Application.Interfaces.Security.CurrentUsers;
using AuthService.Domain.Errors.Users;
using AuthService.Domain.Exceptions;
using AuthService.Domain.Models.Users;
using AuthService.Domain.Resources;
using AuthService.Domain.ViewModels.Basics;
using FluentResults;
using MediatR;

namespace AuthService.Application.Commands.Users.ChangeUsername;

/// <summary>
/// Command handler to change user's username. It's related to <see cref="ChangeUsernameCommand"/>.
/// </summary>
/// <param name="userRepository">Interface to perform user operations in database.</param>
/// <param name="userDetailRepository">Interface to perform user's details operations in database.</param>
/// <param name="currentUserContext">Interface to get information about current signed in user.</param>
public class ChangeUsernameCommandHandler(IUserRepository userRepository,
                                          IUserDetailRepository userDetailRepository,
                                          ICurrentUserContext currentUserContext)
    : IRequestHandler<ChangeUsernameCommand, Result<ViewModelBase>>
{
    /// <summary>
    /// Handles request to change user's username.
    /// </summary>
    /// <param name="request">Request command with details to change user's username.</param>
    /// <param name="cancellationToken">Token to cancel asynchronous operation.</param>
    /// <returns>Instance of <see cref="ViewModelBase"/></returns>
    /// <exception cref="UserUnauthorizedException">When user is not authorized.</exception>
    public async Task<Result<ViewModelBase>> Handle(ChangeUsernameCommand request,
                                                    CancellationToken cancellationToken)
    {
        CurrentAuthorizedUser currentAuthorizedUser = currentUserContext.GetCurrentAuthorizedUser();

        if (await userRepository.UsernameExistsAsync(request.NewUsername, cancellationToken))
        {
            return Result.Fail<ViewModelBase>(new UsernameExistsError());
        }

        await userRepository.UpdateUsernameAsync(currentAuthorizedUser.Id, request.NewUsername, cancellationToken);
        await userDetailRepository.UpdateLastUsernameChangeAsync(currentAuthorizedUser.Id, cancellationToken);

        ViewModelBase result = new(ResponseMessages.UsernameHasBeenChanged);
        return Result.Ok(result);
    }
}