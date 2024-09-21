using Asp.Versioning;
using AuthService.API.Controllers.Basics;
using AuthService.API.ExtensionMethods.Versioning;
using AuthService.Domain.Errors.Users;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Application.AuthService.Commands.Accounts.BanAccountByUserId;
using Shared.Application.AuthService.Commands.Accounts.RemoveAccountBanByUserId;
using Shared.Application.Common.Responses.Basics;
using Shared.Application.Common.Responses.Errors;

namespace AuthService.API.Controllers;

/// <summary>
/// Controller to manage users' accounts.
/// </summary>
/// <param name="mediator">Mediator to handle commands and queries using CQRS pattern.</param>
[ApiVersion(ApiVersioningInfo.Version_1_0)]
public class AccountController(IMediator mediator) : ApiControllerBase
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
    [ProducesResponseType(typeof(ResponseBase), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorDetailsResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(UnauthorizedResult), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ErrorDetailsResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ErrorDetailsResponse), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> BanAccount([FromRoute] int userId,
                                               [FromBody] BanAccountByUserIdRequest banAccountByUserIdRequest,
                                               CancellationToken cancellationToken)
        => await mediator.Send(new BanAccountByUserIdCommand(userId,
                                                             banAccountByUserIdRequest.BanReason,
                                                             banAccountByUserIdRequest.BanNote,
                                                             banAccountByUserIdRequest.BanEndsAt), cancellationToken) switch
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
    /// Endpoint to remove ban from user's account by user id.
    /// </summary>
    /// <param name="userId">User's id.</param>
    /// <param name="cancellationToken">Token to cancel asynchronous operation.</param>
    /// <returns>Information about removing ban from user's account.</returns>
    [Authorize]
    [HttpPut("ban/remove/{userId}")]
    [ProducesResponseType(typeof(ResponseBase), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorDetailsResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(UnauthorizedResult), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ErrorDetailsResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ErrorDetailsResponse), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> RemoveAccountBan([FromRoute] int userId,
                                                      CancellationToken cancellationToken)
        => await mediator.Send(new RemoveAccountBanByUserIdCommand(userId), cancellationToken) switch
        {
            { IsSuccess: true, Value: ResponseBase viewModel } => Ok(viewModel),
            { Errors: { Count: > 0 } errors } => errors[0] switch
            {
                UserNotFoundError => CreateNotFoundObjectResult(errors[0]),
                _ => CreateBadRequestObjectResult(errors)
            },
            _ => CreateBadRequestObjectResult()
        };
}