using CoffeeBlog.Application.Interfaces.Persistence.Repositories;
using CoffeeBlog.Application.Interfaces.Security.CurrentUsers;
using CoffeeBlog.Application.Interfaces.Security.Password;
using CoffeeBlog.Domain.Commands.Users;
using CoffeeBlog.Domain.Exceptions;
using CoffeeBlog.Domain.Models.Users;
using CoffeeBlog.Domain.Resources;
using CoffeeBlog.Domain.ViewModels.Basics;
using FluentResults;
using MediatR;

namespace CoffeeBlog.Application.Handlers.Commands.Users;

public class EditPasswordCommandHandler(IUserRepository _userRepository,
                                        IUserDetailRepository _userDetailRepository,
                                        ICurrentUserContext _currentUserContext,
                                        IPasswordHasher _passwordHasher) : IRequestHandler<EditPasswordCommand, Result<ViewModelBase>>
{
    private readonly IUserRepository _userRepository = _userRepository;
    private readonly IUserDetailRepository _userDetailRepository = _userDetailRepository;
    private readonly ICurrentUserContext _currentUserContext = _currentUserContext;
    private readonly IPasswordHasher _passwordHasher = _passwordHasher;

    /// <summary>
    /// Handles request to edit user's password.
    /// </summary>
    /// <param name="request">Request command with details to edit user's password.</param>
    /// <param name="cancellationToken">Token to cancel asynchronous operation.</param>
    /// <returns>Instance of <see cref="ViewModelBase"/></returns>
    /// <exception cref="UserUnauthorizedException">When user is not authorized.</exception>
    public async Task<Result<ViewModelBase>> Handle(EditPasswordCommand request,
                                                    CancellationToken cancellationToken)
    {
        CurrentAuthorizedUser currentAuthorizedUser = _currentUserContext.GetCurrentAuthorizedUser();

        string hashedPassword = _passwordHasher.HashPassword(request.NewPassword);

        await _userRepository.UpdatePasswordAsync(currentAuthorizedUser.Id, hashedPassword, cancellationToken);

        await _userDetailRepository.UpdateLastEmailChangeAsync(currentAuthorizedUser.Id, cancellationToken);

        ViewModelBase result = new(ResponseMessages.EmailHasBeenChanged);

        return Result.Ok(result);
    }
}