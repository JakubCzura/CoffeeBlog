using CoffeeBlog.Application.Interfaces.Persistence.Repositories;
using CoffeeBlog.Application.Interfaces.Security.CurrentUsers;
using CoffeeBlog.Domain.Commands.Users;
using CoffeeBlog.Domain.Errors.Users;
using CoffeeBlog.Domain.Exceptions;
using CoffeeBlog.Domain.Models.Users;
using CoffeeBlog.Domain.Resources;
using CoffeeBlog.Domain.ViewModels.Basics;
using FluentResults;
using MediatR;

namespace CoffeeBlog.Application.Handlers.Commands.Users;

/// <summary>
/// Command handler to change user's username. It's related to <see cref="ChangeUsernameCommand"/>.
/// </summary>
/// <param name="_userRepository">Interface to perform user operations in database.</param>
/// <param name="_userDetailRepository">Interface to perform user's details operations in database.</param>
/// <param name="_currentUserContext">Interface to get information about current signed in user.</param>
public class ChangeUsernameCommandHandler(IUserRepository _userRepository,
                                          IUserDetailRepository _userDetailRepository,
                                          ICurrentUserContext _currentUserContext) : IRequestHandler<ChangeUsernameCommand, Result<ViewModelBase>>
{
    private readonly IUserRepository _userRepository = _userRepository;
    private readonly IUserDetailRepository _userDetailRepository = _userDetailRepository;
    private readonly ICurrentUserContext _currentUserContext = _currentUserContext;

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
        CurrentAuthorizedUser currentAuthorizedUser = _currentUserContext.GetCurrentAuthorizedUser();

        if (await _userRepository.UsernameExistsAsync(request.NewUsername, cancellationToken))
        {
            return Result.Fail<ViewModelBase>(new UsernameExistsError());
        }

        await _userRepository.UpdateUsernameAsync(currentAuthorizedUser.Id, request.NewUsername, cancellationToken);

        await _userDetailRepository.UpdateLastUsernameChangeAsync(currentAuthorizedUser.Id, cancellationToken);

        ViewModelBase result = new(ResponseMessages.UsernameHasBeenChanged);

        return Result.Ok(result);
    }
}