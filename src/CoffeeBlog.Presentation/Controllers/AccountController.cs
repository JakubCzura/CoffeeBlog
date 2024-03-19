using Asp.Versioning;
using CoffeeBlog.Domain.Commands.Users;
using CoffeeBlog.Domain.Errors.Users;
using CoffeeBlog.Domain.Queries.Users;
using CoffeeBlog.Domain.ViewModels.Basics;
using CoffeeBlog.Domain.ViewModels.Users;
using CoffeeBlog.Presentation.Controllers.Basics;
using CoffeeBlog.Presentation.ExtensionMethods.Versioning;
using FluentResults;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CoffeeBlog.Presentation.Controllers;

/// <summary>
/// This controller is responsible for account related operations like creating new user, changing e-mail or changing username.
/// When defining operations like getting user details or getting list of users, we should use <see cref="UserController"/>.
/// </summary>
[ApiVersion(ApiVersioningInfo.Version_1_0)]
public class AccountController(IMediator _mediator) : ApiControllerBase
{
    private readonly IMediator _mediator = _mediator;

    [AllowAnonymous]
    [HttpPost("signup")]
    public async Task<ActionResult<CreateUserViewModel>> SignUp([FromBody] CreateUserCommand createUserCommand,
                                                                 CancellationToken cancellationToken)
    {
        Result<CreateUserViewModel> result = await _mediator.Send(createUserCommand, cancellationToken);

        if (result.IsSuccess)
        {
            return Ok(result.Value);
        }

        IError error = result.Errors[0];
        return error switch
        {
            UsernameExistsError => Conflict(error.Message),
            EmailExistsError => Conflict(error.Message),
            _ => BadRequest(string.Join(";", result.Errors.Select(x => x.Message)))
        };
    }

    [AllowAnonymous]
    [HttpPost("signin")]
    public async Task<IActionResult> SignIn([FromBody] SignInUserQuery signInUserQuery)
    {
        Result<SignInUserViewModel> result = await _mediator.Send(signInUserQuery);

        if (result.IsSuccess)
        {
            return Ok(result.Value);
        }

        IError error = result.Errors[0];
        return error switch
        {
            UserNotFoundError => Conflict(error.Message),
            _ => BadRequest(string.Join(";", result.Errors.Select(x => x.Message)))
        };
    }

    [Authorize]
    [HttpPut("username/edit")]
    public async Task<IActionResult> EditUsername([FromBody] EditUsernameCommand changeUsernameCommand,
                                                  CancellationToken cancellationToken)
    {
        Result<ViewModelBase> result = await _mediator.Send(changeUsernameCommand, cancellationToken);

        if (result.IsSuccess)
        {
            return Ok(result.Value);
        }

        IError error = result.Errors[0];
        return error switch
        {
            UsernameExistsError => Conflict(error.Message),
            _ => BadRequest(string.Join(";", result.Errors.Select(x => x.Message)))
        };
    }

    [Authorize]
    [HttpPut("email/edit")]
    public async Task<IActionResult> EditEmail([FromBody] EditEmailCommand editEmailCommand,
                                               CancellationToken cancellationToken)
    {
        Result<ViewModelBase> result = await _mediator.Send(editEmailCommand, cancellationToken);

        if (result.IsSuccess)
        {
            return Ok(result.Value);
        }

        IError error = result.Errors[0];
        return error switch
        {
            EmailExistsError => Conflict(error.Message),
            _ => BadRequest(string.Join(";", result.Errors.Select(x => x.Message)))
        };
    }
}