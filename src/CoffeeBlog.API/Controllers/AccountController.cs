using CoffeeBlog.API.Controllers.ControllersBase;
using CoffeeBlog.Application.Commands.Users.CreateUser;
using CoffeeBlog.Application.Queries.Users.LogInUser;
using CoffeeBlog.Application.ViewModels.Users;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CoffeeBlog.API.Controllers;

/// <summary>
/// This controller is responsible for account related operations like creating new user, changing e-mail or changing username.
/// When defining operations like getting user details or getting list of users, we should use <see cref="UserController"/>.
/// </summary>
public class AccountController(IMediator mediator) : ApiControllerBase
{
    private readonly IMediator _mediator = mediator;

    [AllowAnonymous]
    [HttpPost("register/user")]
    public async Task<IActionResult> Register([FromBody] CreateUserCommand createUserCommand,
                                              CancellationToken cancellationToken)
    {
        CreateUserViewModel result = await _mediator.Send(createUserCommand, cancellationToken);
        return Ok(result);
    }

    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LogInUserQuery logInUserCommand)
    {
        //bool isValid = ModelState.IsValid;
        //if (!isValid)
        //{
        //    return BadRequest(string.Join(Environment.NewLine, ModelState.Values.SelectMany(x => x.Errors)));
        //}

        LogInUserViewModel result = await _mediator.Send(logInUserCommand);
        return Ok(result);
    }
}