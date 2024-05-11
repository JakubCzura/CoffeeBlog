using Asp.Versioning;
using AuthService.API.Controllers.Basics;
using AuthService.API.ExtensionMethods.Versioning;
using AuthService.Application.Commands.Accounts.BanAccountByUserId;
using AuthService.Application.Commands.Accounts.RemoveAccountBanByUserId;
using AuthService.Domain.Errors.Users;
using AuthService.Domain.ViewModels.Basics;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AuthService.API.Controllers;

[ApiVersion(ApiVersioningInfo.Version_1_0)]
public class AccountController(IMediator _mediator) : ApiControllerBase(_mediator)
{
    [Authorize]
    [HttpPut("ban/{userId}")]
    public async Task<IActionResult> BanAccount([FromRoute] int userId,
                                                [FromBody] BanAccountByUserIdRequest accountByUserIdRequest,
                                                CancellationToken cancellationToken)
        => await Mediator.Send(new BanAccountByUserIdCommand(userId,
                                                             accountByUserIdRequest.BanReason,
                                                             accountByUserIdRequest.BanNote,
                                                             accountByUserIdRequest.BanEndsAt), cancellationToken) switch
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
    [HttpPut("ban/remove/{userId}")]
    public async Task<IActionResult> RemoveAccountBan([FromRoute] int userId,
                                                      CancellationToken cancellationToken)
        => await Mediator.Send(new RemoveAccountBanByUserIdCommand(userId), cancellationToken) switch
        {
            { IsSuccess: true, Value: ViewModelBase viewModel } => Ok(viewModel),
            { Errors: { Count: > 0 } errors } => errors[0] switch
            {
                UserNotFoundError => Conflict(errors[0].Message),
                _ => BadRequest(string.Join(";", errors.Select(x => x.Message)))
            },
            _ => BadRequest()
        };
}