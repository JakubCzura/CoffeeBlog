using Asp.Versioning;
using AuthService.API.Controllers.Basics;
using AuthService.API.ExtensionMethods.Versioning;
using AuthService.Application.Commands.Users.ChangeEmail;
using AuthService.Application.Commands.Users.ChangePassword;
using AuthService.Application.Commands.Users.ChangeUsername;
using AuthService.Application.Commands.Users.GenerateForgottenPasswordResetToken;
using AuthService.Application.Commands.Users.ResetForgottenPassword;
using AuthService.Application.Commands.Users.SignUpUser;
using AuthService.Application.ExtensionMethods.Collections;
using AuthService.Application.Queries.Users.SignInUser;
using AuthService.Domain.Errors.Users;
using AuthService.Domain.Resources;
using AuthService.Domain.ViewModels.Basics;
using AuthService.Domain.ViewModels.Errors;
using AuthService.Domain.ViewModels.Users;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AuthService.API.Controllers;

[ApiVersion(ApiVersioningInfo.Version_1_0)]
public class UserController(IMediator _mediator) : ApiControllerBase(_mediator)
{

    /// <summary>
    /// Endpoint to sign up user.
    /// </summary>
    /// <param name="signUpUserCommand">Details to create user's account.</param>
    /// <param name="cancellationToken">Token to cancel asynchronous operation.</param>
    /// <returns>Information about signin up user.</returns>
    [AllowAnonymous]
    [HttpPost("sign-up")]
    [ProducesResponseType(typeof(ViewModelBase), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorDetailsViewModel), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorDetailsViewModel), StatusCodes.Status409Conflict)]
    [ProducesResponseType(typeof(ErrorDetailsViewModel), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<SignUpUserViewModel>> SignUp([FromBody] SignUpUserCommand signUpUserCommand,
                                                                CancellationToken cancellationToken)
        => await Mediator.Send(signUpUserCommand, cancellationToken) switch
        {
            { IsSuccess: true, Value: SignUpUserViewModel viewModel } => Ok(viewModel),
            { Errors: { Count: > 0 } errors } => errors[0] switch
            {
                UsernameExistsError => Conflict(new ErrorDetailsViewModel(StatusCodes.Status409Conflict, errors[0].Message)),
                EmailExistsError => Conflict(new ErrorDetailsViewModel(StatusCodes.Status409Conflict, errors[0].Message)),
                _ => BadRequest(new ErrorDetailsViewModel(StatusCodes.Status400BadRequest, errors.GetJoinedMessages()))
            },
            _ => BadRequest(new ErrorDetailsViewModel(StatusCodes.Status400BadRequest, ResponseMessages.UndefinedError))
        };

    /// <summary>
    /// Endpoint to sign in user.
    /// </summary>
    /// <param name="signInUserQuery">User's credentials.</param>
    /// <param name="cancellationToken">Token to cancel asynchronous operation.</param>
    /// <returns>Result of attempt to sign in and authorization token for successful attempt.</returns>
    [AllowAnonymous]
    [HttpPost("sign-in")]
    [ProducesResponseType(typeof(ViewModelBase), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorDetailsViewModel), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(UnauthorizedResult), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ErrorDetailsViewModel), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ErrorDetailsViewModel), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> SignIn([FromBody] SignInUserQuery signInUserQuery,
                                            CancellationToken cancellationToken)
        => await Mediator.Send(signInUserQuery, cancellationToken) switch
        {
            { IsSuccess: true, Value: SignInUserViewModel viewModel } => Ok(viewModel),
            { Errors: { Count: > 0 } errors } => errors[0] switch
            {
                UserNotFoundError => NotFound(new ErrorDetailsViewModel(StatusCodes.Status404NotFound, errors[0].Message)),
                UserBannedError => Unauthorized(new ErrorDetailsViewModel(StatusCodes.Status401Unauthorized, errors[0].Message)),
                _ => BadRequest(new ErrorDetailsViewModel(StatusCodes.Status400BadRequest, errors.GetJoinedMessages()))
            },
            _ => BadRequest(new ErrorDetailsViewModel(StatusCodes.Status400BadRequest, ResponseMessages.UndefinedError))
        };

    /// <summary>
    /// Endpoint to change user's username.
    /// </summary>
    /// <param name="changeUsernameCommand">Details to set new username.</param>
    /// <param name="cancellationToken">Token to cancel asynchronous operation.</param>
    /// <returns>Information about changing user's username.</returns>
    [Authorize]
    [HttpPut("username")]
    [ProducesResponseType(typeof(ViewModelBase), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorDetailsViewModel), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(UnauthorizedResult), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ErrorDetailsViewModel), StatusCodes.Status409Conflict)]
    [ProducesResponseType(typeof(ErrorDetailsViewModel), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> ChangeUsername([FromBody] ChangeUsernameCommand changeUsernameCommand,
                                                    CancellationToken cancellationToken)
        => await Mediator.Send(changeUsernameCommand, cancellationToken) switch
        {
            { IsSuccess: true, Value: ViewModelBase viewModel } => Ok(viewModel),
            { Errors: { Count: > 0 } errors } => errors[0] switch
            {
                UsernameExistsError => Conflict(new ErrorDetailsViewModel(StatusCodes.Status409Conflict, errors[0].Message)),
                _ => BadRequest(new ErrorDetailsViewModel(StatusCodes.Status400BadRequest, errors.GetJoinedMessages()))
            },
            _ => BadRequest(new ErrorDetailsViewModel(StatusCodes.Status400BadRequest, ResponseMessages.UndefinedError))
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
                _ => BadRequest(errors.GetJoinedMessages())
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
            { Errors: { Count: > 0 } errors } => BadRequest(errors.GetJoinedMessages()),
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
                UserNotFoundError => NotFound(errors[0].Message),
                _ => BadRequest(errors.GetJoinedMessages())
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
                UserNotFoundError => NotFound(errors[0].Message),
                InvalidForgottenPasswordResetTokenError => Forbid(errors[0].Message),
                ExpiredForgottenPasswordResetTokenError => Forbid(errors[0].Message),
                _ => BadRequest(errors.GetJoinedMessages())
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
                UserNotFoundError => NotFound(errors[0].Message),
                InvalidForgottenPasswordResetTokenError => Forbid(errors[0].Message),
                ExpiredForgottenPasswordResetTokenError => Forbid(errors[0].Message),
                _ => BadRequest(errors.GetJoinedMessages())
            },
            _ => BadRequest()
        };
}