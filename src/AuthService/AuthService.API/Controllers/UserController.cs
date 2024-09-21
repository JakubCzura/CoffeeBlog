using Asp.Versioning;
using AuthService.API.Controllers.Basics;
using AuthService.API.ExtensionMethods.Versioning;
using AuthService.Domain.Errors.Users;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Application.AuthService.Commands.Users.ChangeEmail;
using Shared.Application.AuthService.Commands.Users.ChangePassword;
using Shared.Application.AuthService.Commands.Users.ChangeUsername;
using Shared.Application.AuthService.Commands.Users.GenerateForgottenPasswordResetToken;
using Shared.Application.AuthService.Commands.Users.ResetForgottenPassword;
using Shared.Application.AuthService.Commands.Users.SignUpUser;
using Shared.Application.AuthService.Queries.Users.SignInUser;
using Shared.Application.AuthService.Responses.Users;
using Shared.Application.Common.Responses.Basics;
using Shared.Application.Common.Responses.Errors;

namespace AuthService.API.Controllers;

/// <summary>
/// Controller to manage users.
/// </summary>
/// <param name="mediator">Mediator to handle commands and queries using CQRS pattern.</param>
[ApiVersion(ApiVersioningInfo.Version_1_0)]
public class UserController(IMediator mediator) : ApiControllerBase
{
    /// <summary>
    /// Endpoint to sign up user.
    /// </summary>
    /// <param name="signUpUserCommand">Details to create user's account.</param>
    /// <param name="cancellationToken">Token to cancel asynchronous operation.</param>
    /// <returns>Information about signin up user.</returns>
    [AllowAnonymous]
    [HttpPost("sign-up")]
    [ProducesResponseType(typeof(SignUpUserResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorDetailsResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorDetailsResponse), StatusCodes.Status409Conflict)]
    [ProducesResponseType(typeof(ErrorDetailsResponse), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<SignUpUserResponse>> SignUp([FromBody] SignUpUserCommand signUpUserCommand,
                                                               CancellationToken cancellationToken)
        => await mediator.Send(signUpUserCommand, cancellationToken) switch
        {
            { IsSuccess: true, Value: SignUpUserResponse viewModel } => Ok(viewModel),
            { Errors: { Count: > 0 } errors } => errors[0] switch
            {
                UsernameExistsError => CreateConflictObjectResult(errors[0]),
                EmailExistsError => CreateConflictObjectResult(errors[0]),
                _ => CreateBadRequestObjectResult(errors)
            },
            _ => CreateBadRequestObjectResult()
        };

    /// <summary>
    /// Endpoint to sign in user.
    /// </summary>
    /// <param name="signInUserQuery">User's credentials.</param>
    /// <param name="cancellationToken">Token to cancel asynchronous operation.</param>
    /// <returns>Result of attempt to sign in and authorization token for successful attempt.</returns>
    [AllowAnonymous]
    [HttpPost("sign-in")]
    [ProducesResponseType(typeof(SignInUserResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorDetailsResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(UnauthorizedResult), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ErrorDetailsResponse), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> SignIn([FromBody] SignInUserQuery signInUserQuery,
                                            CancellationToken cancellationToken)
        => await mediator.Send(signInUserQuery, cancellationToken) switch
        {
            { IsSuccess: true, Value: SignInUserResponse viewModel } => Ok(viewModel),
            { Errors: { Count: > 0 } errors } => errors[0] switch
            {
                UserNotFoundError => CreateUnauthorizedObjectResult(errors[0]),
                UserBannedError => CreateUnauthorizedObjectResult(errors[0]),
                _ => CreateBadRequestObjectResult(errors)
            },
            _ => CreateBadRequestObjectResult()
        };

    /// <summary>
    /// Endpoint to change user's username.
    /// </summary>
    /// <param name="changeUsernameCommand">Details to set new username.</param>
    /// <param name="cancellationToken">Token to cancel asynchronous operation.</param>
    /// <returns>Information about changing user's username.</returns>
    [Authorize]
    [HttpPut("username")]
    [ProducesResponseType(typeof(ResponseBase), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorDetailsResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(UnauthorizedResult), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ErrorDetailsResponse), StatusCodes.Status409Conflict)]
    [ProducesResponseType(typeof(ErrorDetailsResponse), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> ChangeUsername([FromBody] ChangeUsernameCommand changeUsernameCommand,
                                                    CancellationToken cancellationToken)
        => await mediator.Send(changeUsernameCommand, cancellationToken) switch
        {
            { IsSuccess: true, Value: ResponseBase viewModel } => Ok(viewModel),
            { Errors: { Count: > 0 } errors } => errors[0] switch
            {
                UsernameExistsError => CreateConflictObjectResult(errors[0]),
                _ => CreateBadRequestObjectResult(errors)
            },
            _ => CreateBadRequestObjectResult()
        };

    /// <summary>
    /// Endpoint to change user's e-mail.
    /// </summary>
    /// <param name="changeEmailCommand">Details to set new e-mail.</param>
    /// <param name="cancellationToken">Token to cancel asynchronous operation.</param>
    /// <returns>Information about changing user's e-mail.</returns>
    [Authorize]
    [HttpPut("email")]
    [ProducesResponseType(typeof(ResponseBase), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorDetailsResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(UnauthorizedResult), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ErrorDetailsResponse), StatusCodes.Status409Conflict)]
    [ProducesResponseType(typeof(ErrorDetailsResponse), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> ChangeEmail([FromBody] ChangeEmailCommand changeEmailCommand,
                                                 CancellationToken cancellationToken)
        => await mediator.Send(changeEmailCommand, cancellationToken) switch
        {
            { IsSuccess: true, Value: ErrorDetailsResponse viewModel } => Ok(viewModel),
            { Errors: { Count: > 0 } errors } => errors[0] switch
            {
                EmailExistsError => CreateConflictObjectResult(errors[0]),
                _ => CreateBadRequestObjectResult(errors)
            },
            _ => CreateBadRequestObjectResult()
        };

    /// <summary>
    /// Endpoint to change user's password.
    /// </summary>
    /// <param name="changePasswordCommand">Details to set new password.</param>
    /// <param name="cancellationToken">Token to cancel asynchronous operation.</param>
    /// <returns>Information about changing user's password.</returns>
    [Authorize]
    [HttpPut("password")]
    [ProducesResponseType(typeof(ResponseBase), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorDetailsResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(UnauthorizedResult), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ErrorDetailsResponse), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordCommand changePasswordCommand,
                                                    CancellationToken cancellationToken)
        => await mediator.Send(changePasswordCommand, cancellationToken) switch
        {
            { IsSuccess: true, Value: ResponseBase viewModel } => Ok(viewModel),
            { Errors: { Count: > 0 } errors } => CreateBadRequestObjectResult(errors),
            _ => CreateBadRequestObjectResult()
        };

    /// <summary>
    /// Endpoint to send password reset token that is necessary to validate user's identity when attempting to reset password.
    /// </summary>
    /// <param name="generateForgottenPasswordResetTokenCommand">Details to send password reset token.</param>
    /// <param name="cancellationToken">Token to cancel asynchronous operation.</param>
    /// <returns>Information about sending password reset token.</returns>
    [AllowAnonymous]
    [HttpPost("forgotten-password-reset-token")]
    [ProducesResponseType(typeof(ResponseBase), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorDetailsResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorDetailsResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ErrorDetailsResponse), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GenerateForgottenPasswordResetToken([FromBody] GenerateForgottenPasswordResetTokenCommand generateForgottenPasswordResetTokenCommand,
                                                                         CancellationToken cancellationToken)
        => await mediator.Send(generateForgottenPasswordResetTokenCommand, cancellationToken) switch
        {
            { IsSuccess: true, Value: ResponseBase viewModel } => Ok(viewModel),
            { Errors: { Count: > 0 } errors } => errors[0] switch
            {
                UserNotFoundError => CreateNotFoundObjectResult(errors[0]),
                _ => CreateBadRequestObjectResult(errors)
            },
            _ => CreateBadRequestObjectResult()
        };

    /// <summary>
    /// Endpoint to reset user's forgotten password.
    /// </summary>
    /// <param name="resetForgottenPasswordCommand">Details to reset password.</param>
    /// <param name="cancellationToken">Token to cancel asynchronous operation.</param>
    /// <returns>Information about resetting password.</returns>
    [AllowAnonymous]
    [HttpPut("password-reset")]
    [ProducesResponseType(typeof(ResponseBase), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorDetailsResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorDetailsResponse), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ErrorDetailsResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ErrorDetailsResponse), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> ResetPassword([FromBody] ResetForgottenPasswordCommand resetForgottenPasswordCommand,
                                                   CancellationToken cancellationToken)
        => await mediator.Send(resetForgottenPasswordCommand, cancellationToken) switch
        {
            { IsSuccess: true, Value: ResponseBase viewModel } => Ok(viewModel),
            { Errors: { Count: > 0 } errors } => errors[0] switch
            {
                UserNotFoundError => CreateNotFoundObjectResult(errors[0]),
                InvalidForgottenPasswordResetTokenError => CreateUnauthorizedObjectResult(errors[0]),
                ExpiredForgottenPasswordResetTokenError => CreateUnauthorizedObjectResult(errors[0]),
                _ => CreateBadRequestObjectResult(errors)
            },
            _ => CreateBadRequestObjectResult()
        };
}