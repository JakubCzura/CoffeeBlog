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

public class EditEmailCommandHandler(IUserRepository _userRepository,
                                     IUserDetailRepository _userDetailRepository,
                                     ICurrentUserContext _currentUserContext) : IRequestHandler<EditEmailCommand, Result<ViewModelBase>>
{
    private readonly IUserRepository _userRepository = _userRepository;
    private readonly IUserDetailRepository _userDetailRepository = _userDetailRepository;
    private readonly ICurrentUserContext _currentUserContext = _currentUserContext;

    /// <summary>
    /// Handles request to edit user's e-mail.
    /// </summary>
    /// <param name="request">Request command with details to edit user's e-mail.</param>
    /// <param name="cancellationToken">Token to cancel asynchronous operation.</param>
    /// <returns>Instance of <see cref="ViewModelBase"/></returns>
    /// <exception cref="UserUnauthorizedException">When user is not authorized.</exception>
    public async Task<Result<ViewModelBase>> Handle(EditEmailCommand request,
                                                    CancellationToken cancellationToken)
    {
        CurrentAuthorizedUser currentAuthorizedUser = _currentUserContext.GetCurrentAuthorizedUser();

        if (await _userRepository.EmailExistsAsync(request.NewEmail, cancellationToken))
        {
            return Result.Fail<ViewModelBase>(new EmailExistsError());
        }

        await _userRepository.UpdateEmailAsync(currentAuthorizedUser.Id, request.NewEmail, cancellationToken);

        await _userDetailRepository.UpdateLastEmailChangeAsync(currentAuthorizedUser.Id, cancellationToken);

        ViewModelBase result = new(ResponseMessages.EmailHasBeenChanged);

        return Result.Ok(result);
    }
}