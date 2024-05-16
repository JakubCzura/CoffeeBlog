using Asp.Versioning;
using AuthService.API.Controllers.Basics;
using AuthService.API.ExtensionMethods.Versioning;
using AuthService.Application.Commands.Accounts.BanAccountByUserId;
using AuthService.Application.Commands.Accounts.RemoveAccountBanByUserId;
using AuthService.Application.ExtensionMethods.Collections;
using AuthService.Domain.Errors.Users;
using AuthService.Domain.Resources;
using AuthService.Domain.ViewModels.Basics;
using AuthService.Domain.ViewModels.Errors;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AuthService.API.Controllers;

/// <summary>
/// Controller to manage user's account.
/// </summary>
/// <param name="_mediator">Mediator to handle requests' commands and queries.</param>
[ApiVersion(ApiVersioningInfo.Version_1_0)]
public class AccountController(IMediator _mediator) : ApiControllerBase(_mediator)
{
    /// <summary>
    /// Endpoint to ban user's account by user id.
    /// </summary>
    /// <param name="userId">User's id.</param>
    /// <param name="banAccountByUserIdRequest">Request body with details to ban user's account.</param>
    /// <param name="cancellationToken">Token to cancel asynchronous operation.</param>
    /// <returns>Information about banning user's account.</returns>
    [Authorize]
    [HttpPut("ban/{userId}")]
    [ProducesResponseType(typeof(ViewModelBase), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorDetailsViewModel), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(UnauthorizedResult), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ErrorDetailsViewModel), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ErrorDetailsViewModel), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> BanAccount([FromRoute] int userId,
                                               [FromBody] BanAccountByUserIdRequest banAccountByUserIdRequest,
                                               CancellationToken cancellationToken)
        => await Mediator.Send(new BanAccountByUserIdCommand(userId,
                                                             banAccountByUserIdRequest.BanReason,
                                                             banAccountByUserIdRequest.BanNote,
                                                             banAccountByUserIdRequest.BanEndsAt), cancellationToken) switch
        {
            { IsSuccess: true, Value: ViewModelBase viewModel } => Ok(viewModel),
            { Errors: { Count: > 0 } errors } => errors[0] switch
            {
                UserNotFoundError => NotFound(new ErrorDetailsViewModel(StatusCodes.Status404NotFound, errors[0].Message)),
                _ => BadRequest(new ErrorDetailsViewModel(StatusCodes.Status400BadRequest, errors.GetJoinedMessages()))
            },
            _ => BadRequest(new ErrorDetailsViewModel(StatusCodes.Status400BadRequest, ResponseMessages.UndefinedError))
        };

    /// <summary>
    /// Endpoint to remove ban from user's account by user id.
    /// </summary>
    /// <param name="userId">User's id.</param>
    /// <param name="cancellationToken">Token to cancel asynchronous operation.</param>
    /// <returns>Information about removing ban from user's account.</returns>
    [Authorize]
    [HttpPut("ban/remove/{userId}")]
    [ProducesResponseType(typeof(ViewModelBase), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorDetailsViewModel), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(UnauthorizedResult), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ErrorDetailsViewModel), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ErrorDetailsViewModel), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> RemoveAccountBan([FromRoute] int userId,
                                                      CancellationToken cancellationToken)
        => await Mediator.Send(new RemoveAccountBanByUserIdCommand(userId), cancellationToken) switch
        {
            { IsSuccess: true, Value: ViewModelBase viewModel } => Ok(viewModel),
            { Errors: { Count: > 0 } errors } => errors[0] switch
            {
                UserNotFoundError => NotFound(new ErrorDetailsViewModel(StatusCodes.Status404NotFound, errors[0].Message)),
                _ => BadRequest(new ErrorDetailsViewModel(StatusCodes.Status400BadRequest, errors.GetJoinedMessages()))
            },
            _ => BadRequest(new ErrorDetailsViewModel(StatusCodes.Status400BadRequest, ResponseMessages.UndefinedError))
        };
}