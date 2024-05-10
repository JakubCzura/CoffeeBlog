using Asp.Versioning;
using AuthService.API.Controllers.Basics;
using AuthService.API.ExtensionMethods.Versioning;
using AuthService.Application.Commands.Accounts.BanAccountByUserId;
using AuthService.Application.Commands.Accounts.RemoveAccountBanByUserId;
using AuthService.Domain.Errors.Users;
using AuthService.Domain.ViewModels.Basics;
using FluentResults;
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
    {
        BanAccountByUserIdCommand banAccountByUserIdCommand = new(userId,
                                                                  accountByUserIdRequest.BanReason,
                                                                  accountByUserIdRequest.BanNote,
                                                                  accountByUserIdRequest.BanEndsAt);

        Result<ViewModelBase> result = await Mediator.Send(banAccountByUserIdCommand, cancellationToken);

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
    [HttpPut("ban/remove/{userId}")]
    public async Task<IActionResult> RemoveAccountBan([FromRoute] int userId,
                                                      CancellationToken cancellationToken)
    {
        RemoveAccountBanByUserIdCommand removeAccountBanByUserIdCommand = new(userId);

        Result<ViewModelBase> result = await Mediator.Send(removeAccountBanByUserIdCommand, cancellationToken);

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
}