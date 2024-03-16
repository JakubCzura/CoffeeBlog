using AutoMapper;
using CoffeeBlog.Application.Interfaces.Helpers;
using CoffeeBlog.Application.Interfaces.Persistence.Repositories;
using CoffeeBlog.Domain.Commands.Users;
using CoffeeBlog.Domain.Errors.Users;
using CoffeeBlog.Domain.ViewModels.Basics;
using FluentResults;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace CoffeeBlog.Application.Handlers.Commands.Users;

public class ChangeUsernameCommandHandler(IUserRepository _userRepository,
                                          IUserDetailRepository _userDetailRepository,
                                          IMapper _mapper,
                                          IDateTimeProvider _dateTimeProvider) : IRequestHandler<ChangeUsernameCommand, Result<ViewModelBase>>
{
    private readonly IUserRepository _userRepository = _userRepository;
    private readonly IUserDetailRepository _userDetailRepository = _userDetailRepository;
    private readonly IMapper _mapper = _mapper;
    private readonly IDateTimeProvider _dateTimeProvider = _dateTimeProvider;

    /// <summary>
    /// Handles request to change user's username.
    /// </summary>
    /// <param name="request">Request command with details to change user's username.</param>
    /// <param name="cancellationToken">Token to cancel asynchronous operation.</param>
    /// <returns>Instance of <see cref="ViewModelBase"/></returns>
    /// <exception cref="Exception">When username or e-mail already exists in database or username and e-mail are the same.</exception>
    public async Task<Result<ViewModelBase>> Handle(ChangeUsernameCommand request,
                                                    CancellationToken cancellationToken)
    {
        if (await _userRepository.UsernameExistsAsync(request.NewUsername, cancellationToken))
        {
            return Result.Fail<ViewModelBase>(new UsernameExistsError());
        }
       
        throw new NotImplementedException();
        int userId = 999999;
       
        await _userRepository.UpdateUsernameAsync(userId, request.NewUsername, cancellationToken);

        await _userDetailRepository.UpdateLastUsernameChangeAsync(userId, cancellationToken);

        ViewModelBase result = new("Username has been changed");

        return Result.Ok(result);
    }
}