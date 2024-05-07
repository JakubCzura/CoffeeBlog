using AuthService.Application.Interfaces.Persistence.Repositories;
using AuthService.Application.Interfaces.Security.CurrentUsers;
using AuthService.Application.Interfaces.Security.Password;
using AuthService.Domain.Errors.Users;
using AuthService.Domain.Exceptions;
using AuthService.Domain.Models.Users;
using AuthService.Domain.Resources;
using AuthService.Domain.ViewModels.Basics;
using FluentResults;
using MediatR;

namespace AuthService.Application.Commands.Users.ChangePassword;

/// <summary>
/// Command handler to change user's password. It's related to <see cref="ChangePasswordCommand"/>.
/// </summary>
/// <param name="_userRepository">Interface to perform user operations in database.</param>
/// <param name="_userDetailRepository">Interface to perform user's details operations in database.</param>
/// <param name="_userLastPasswordRepository">Interface to perform user's last passwords operations in database.</param>
/// <param name="_currentUserContext">Interface to get information about current signed in user.</param>
/// <param name="_passwordHasher">Interface hash user's password.</param>
public class ChangePasswordCommandHandler(IUserRepository _userRepository,
                                         IUserDetailRepository _userDetailRepository,
                                         IUserLastPasswordRepository _userLastPasswordRepository,
                                         ICurrentUserContext _currentUserContext,
                                         IPasswordHasher _passwordHasher)
    : IRequestHandler<ChangePasswordCommand, Result<ViewModelBase>>
{
    private readonly IUserRepository _userRepository = _userRepository;
    private readonly IUserDetailRepository _userDetailRepository = _userDetailRepository;
    private readonly IUserLastPasswordRepository _userLastPasswordRepository = _userLastPasswordRepository;
    private readonly ICurrentUserContext _currentUserContext = _currentUserContext;
    private readonly IPasswordHasher _passwordHasher = _passwordHasher;

    /// <summary>
    /// Handles request to change user's password.
    /// </summary>
    /// <param name="request">Request command with details to change user's password.</param>
    /// <param name="cancellationToken">Token to cancel asynchronous operation.</param>
    /// <returns>Instance of <see cref="ViewModelBase"/></returns>
    /// <exception cref="UserUnauthorizedException">When user is not authorized.</exception>
    public async Task<Result<ViewModelBase>> Handle(ChangePasswordCommand request,
                                                    CancellationToken cancellationToken)
    {
        CurrentAuthorizedUser currentAuthorizedUser = _currentUserContext.GetCurrentAuthorizedUser();

        string hashedPassword = _passwordHasher.HashPassword(request.NewPassword);

        IEnumerable<string> userLastPasswords = (await _userLastPasswordRepository.GetLastPasswordsByUserIdAsync(currentAuthorizedUser.Id, cancellationToken))
                                                                                  .Select(userLastPassword => userLastPassword.LastPassword);

        if (userLastPasswords.Contains(hashedPassword))
        {
            return Result.Fail<ViewModelBase>(new PasswordAlreadyUsedError());
        }

        await _userRepository.UpdatePasswordAsync(currentAuthorizedUser.Id, hashedPassword, cancellationToken);

        await _userLastPasswordRepository.CreateAsync(new(hashedPassword, currentAuthorizedUser.Id), cancellationToken);

        await _userDetailRepository.UpdateLastPasswordChangeAsync(currentAuthorizedUser.Id, cancellationToken);

        ViewModelBase result = new(ResponseMessages.PasswordHasBeenChanged);

        return Result.Ok(result);
    }
}