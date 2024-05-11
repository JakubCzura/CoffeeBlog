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
        => await Mediator.Send(signUpUserCommand, cancellationToken) switch
        {
            { IsSuccess: true, Value: SignUpUserViewModel viewModel } => Ok(viewModel),
            { Errors: { Count: > 0 } errors } => errors[0] switch
            {
                UsernameExistsError => Conflict(errors[0].Message),
                EmailExistsError => Conflict(errors[0].Message),
                _ => BadRequest(string.Join(";", errors.Select(x => x.Message)))
            },
            _ => BadRequest()
        };

    [AllowAnonymous]
    [HttpPost("sign-in")]
    public async Task<IActionResult> SignIn([FromBody] SignInUserQuery signInUserQuery)
        => await Mediator.Send(signInUserQuery) switch
        {
            { IsSuccess: true, Value: SignInUserViewModel viewModel } => Ok(viewModel),
            { Errors: { Count: > 0 } errors } => errors[0] switch
            {
                UserNotFoundError => Conflict(errors[0].Message),
                UserBannedError => Forbid(errors[0].Message),
                _ => BadRequest(string.Join(";", errors.Select(x => x.Message)))
            },
            _ => BadRequest()
        };

    [Authorize]
    [HttpPut("username")]
    public async Task<IActionResult> ChangeUsername([FromBody] ChangeUsernameCommand changeUsernameCommand,
                                                    CancellationToken cancellationToken)
        => await Mediator.Send(changeUsernameCommand, cancellationToken) switch
        {
            { IsSuccess: true, Value: ViewModelBase viewModel } => Ok(viewModel),
            { Errors: { Count: > 0 } errors } => errors[0] switch
            {
                UsernameExistsError => Conflict(errors[0].Message),
                _ => BadRequest(string.Join(";", errors.Select(x => x.Message)))
            },
            _ => BadRequest()
        };

    [Authorize]
    [HttpPut("email")]
    public async Task<IActionResult> ChangeEmail([FromBody] ChangeEmailCommand changeEmailCommand,
                                                 CancellationToken cancellationToken)
        => await Mediator.Send(changeEmailCommand, cancellationToken) switch
        {
            { IsSuccess: true, Value: ViewModelBase viewModel } => Ok(viewModel),
            { Errors: { Count: > 0 } errors } => errors[0] switch
            {
                EmailExistsError => Conflict(errors[0].Message),
                _ => BadRequest(string.Join(";", errors.Select(x => x.Message)))
            },
            _ => BadRequest()
        };

    [Authorize]
    [HttpPut("password")]
    public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordCommand changePasswordCommand,
                                                    CancellationToken cancellationToken)
        => await Mediator.Send(changePasswordCommand, cancellationToken) switch
        {
            { IsSuccess: true, Value: ViewModelBase viewModel } => Ok(viewModel),
            { Errors: { Count: > 0 } errors } => BadRequest(string.Join(";", errors.Select(x => x.Message))),
            _ => BadRequest()
        };

    [Authorize]
    [HttpPost("forgotten-password-reset-token")]
    public async Task<IActionResult> GenerateForgottenPasswordResetToken([FromBody] GenerateForgottenPasswordResetTokenCommand generateForgottenPasswordResetTokenCommand,
                                                                         CancellationToken cancellationToken)
        => await Mediator.Send(generateForgottenPasswordResetTokenCommand, cancellationToken) switch
        {
            { IsSuccess: true, Value: ViewModelBase viewModel } => Ok(viewModel),
            { Errors: { Count: > 0 } errors } => errors[0] switch
            {
                UserNotFoundError => Conflict(errors[0].Message),
                _ => BadRequest(string.Join(";", errors.Select(x => x.Message)))
            },
            _ => BadRequest()
        };

    [Authorize]
    [HttpPost("password-reset")]
    public async Task<IActionResult> ResetPassword([FromBody] ResetForgottenPasswordCommand resetForgottenPasswordCommand,
                                                   CancellationToken cancellationToken)
        => await Mediator.Send(resetForgottenPasswordCommand, cancellationToken) switch
        {
            { IsSuccess: true, Value: ViewModelBase viewModel } => Ok(viewModel),
            { Errors: { Count: > 0 } errors } => errors[0] switch
            {
                UserNotFoundError => Conflict(errors[0].Message),
                InvalidForgottenPasswordResetTokenError => Forbid(errors[0].Message),
                ExpiredForgottenPasswordResetTokenError => Forbid(errors[0].Message),
                _ => BadRequest(string.Join(";", errors.Select(x => x.Message)))
            },
            _ => BadRequest()
        };

    [Authorize]
    [HttpPost("password-reset2")]
    public async Task<IActionResult> ResetPassword2([FromBody] ResetForgottenPasswordCommand resetForgottenPasswordCommand,
                                                   CancellationToken cancellationToken)

        => await Mediator.Send(resetForgottenPasswordCommand, cancellationToken) switch
        {
            { IsSuccess: true, Value: ViewModelBase viewModel } => Ok(viewModel),
            { Errors: { Count: > 0 } errors } => errors[0] switch
            {
                UserNotFoundError => Conflict(errors[0].Message),
                InvalidForgottenPasswordResetTokenError => Forbid(errors[0].Message),
                ExpiredForgottenPasswordResetTokenError => Forbid(errors[0].Message),
                _ => BadRequest(string.Join(";", errors.Select(x => x.Message)))
            },
            _ => BadRequest()
        };
}