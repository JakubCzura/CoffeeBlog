using Asp.Versioning;
using CoffeeBlog.Domain.Commands.Users;
using CoffeeBlog.Domain.Errors.Users;
using CoffeeBlog.Domain.Queries.Users;
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
public class AccountController(IMediator mediator) : ApiControllerBase
{
    private readonly IMediator _mediator = mediator;

    [AllowAnonymous]
    [HttpPost("register/user")]
    public async Task<ActionResult<CreateUserViewModel>> Register([FromBody] CreateUserCommand createUserCommand,
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
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] SignInUserQuery logInUserCommand)
    {
        //bool isValid = ModelState.IsValid;
        //if (!isValid)
        //{
        //    return BadRequest(string.Join(Environment.NewLine, ModelState.Values.SelectMany(x => x.Errors)));
        //}

        SignInUserViewModel result = await _mediator.Send(logInUserCommand);
        return Ok(result);
    }
}