using Asp.Versioning;
using AuthService.API.Controllers.Basics;
using AuthService.API.ExtensionMethods.Versioning;
using AuthService.Application.Commands.Users.ChangeEmail;
using AuthService.Application.Commands.Users.ChangePassword;
using AuthService.Application.Commands.Users.ChangeUsername;
using AuthService.Application.Commands.Users.GenerateForgottenPasswordResetToken;
using AuthService.Application.Commands.Users.ResetForgottenPassword;
using AuthService.Application.Commands.Users.SignUpUser;
using AuthService.Application.Queries.Users.SignInUser;
using AuthService.Domain.Errors.Users;
using AuthService.Domain.ViewModels.Basics;
using AuthService.Domain.ViewModels.Users;
using FluentResults;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AuthService.API.Controllers;

[ApiVersion(ApiVersioningInfo.Version_1_0)]
public class UserController(IMediator _mediator) : ApiControllerBase(_mediator)
{
    [AllowAnonymous]
    [HttpPost("sign-up")]
    public async Task<ActionResult<SignUpUserViewModel>> SignUp([FromBody] SignUpUserCommand signUpUserCommand,
                                                                CancellationToken cancellationToken)
    {
        Result<SignUpUserViewModel> result = await Mediator.Send(signUpUserCommand, cancellationToken);

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
    [HttpPost("sign-in")]
    public async Task<IActionResult> SignIn([FromBody] SignInUserQuery signInUserQuery)
    {
        Result<SignInUserViewModel> result = await Mediator.Send(signInUserQuery);

        if (result.IsSuccess)
        {
            return Ok(result.Value);
        }

        IError error = result.Errors[0];
        return error switch
        {
            UserNotFoundError => Conflict(error.Message),
            UserBannedError => Forbid(error.Message),
            _ => BadRequest(string.Join(";", result.Errors.Select(x => x.Message)))
        };
    }

    [Authorize]
    [HttpPut("username")]
    public async Task<IActionResult> ChangeUsername([FromBody] ChangeUsernameCommand changeUsernameCommand,
                                                    CancellationToken cancellationToken)
    {
        Result<ViewModelBase> result = await Mediator.Send(changeUsernameCommand, cancellationToken);

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
    [HttpPut("email")]
    public async Task<IActionResult> ChangeEmail([FromBody] ChangeEmailCommand changeEmailCommand,
                                                 CancellationToken cancellationToken)
    {
        Result<ViewModelBase> result = await Mediator.Send(changeEmailCommand, cancellationToken);

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

    [Authorize]
    [HttpPut("password")]
    public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordCommand changePasswordCommand,
                                                    CancellationToken cancellationToken)
    {
        Result<ViewModelBase> result = await Mediator.Send(changePasswordCommand, cancellationToken);

        if (result.IsSuccess)
        {
            return Ok(result.Value);
        }

        return BadRequest(string.Join(";", result.Errors.Select(x => x.Message)));
    }

    [Authorize]
    [HttpPost("forgotten-password-reset-token")]
    public async Task<IActionResult> GenerateForgottenPasswordResetToken([FromBody] GenerateForgottenPasswordResetTokenCommand generateForgottenPasswordResetTokenCommand,
                                                                         CancellationToken cancellationToken)
    {
        Result<ViewModelBase> result = await Mediator.Send(generateForgottenPasswordResetTokenCommand, cancellationToken);

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
    [HttpPost("password-reset")]
    public async Task<IActionResult> ResetPassword([FromBody] ResetForgottenPasswordCommand resetForgottenPasswordCommand,
                                                   CancellationToken cancellationToken)
    {
        Result<ViewModelBase> result = await Mediator.Send(resetForgottenPasswordCommand, cancellationToken);

        if (result.IsSuccess)
        {
            return Ok(result.Value);
        }

        IError error = result.Errors[0];
        return error switch
        {
            UserNotFoundError => Conflict(error.Message),
            InvalidForgottenPasswordResetTokenError => Forbid(error.Message),
            ExpiredForgottenPasswordResetTokenError => Forbid(error.Message),
            _ => BadRequest(string.Join(";", result.Errors.Select(x => x.Message)))
        };
    }
}