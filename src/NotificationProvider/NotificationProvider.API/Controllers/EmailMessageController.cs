using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NotificationProvider.API.Controllers.Basics;
using NotificationProvider.API.ExtensionMethods.Versioning;
using Shared.Application.Common.Responses.Basics;
using Shared.Application.Common.Responses.Errors;
using Shared.Application.NotificationProvider.Commands.EmailMessages.ContactUs;

namespace NotificationProvider.API.Controllers;

/// <summary>
/// Controller to manage email messages.
/// </summary>
/// <param name="mediator">Mediator to handle command</param>
[ApiVersion(ApiVersioningInfo.Version_1_0)]
public class EmailMessageController(IMediator mediator) : ApiControllerBase
{
    /// <summary>
    /// Endpoint to contact with CoffeeBlog.
    /// </summary>
    /// <param name="contactUsCommand">Details to contact with CoffeeBlog.</param>
    /// <param name="cancellationToken">Token to cancel asynchronous operation.</param>
    /// <returns>Information about contacting with CoffeeBlog.</returns>
    [AllowAnonymous]
    [HttpPost("contact")]
    [ProducesResponseType(typeof(ResponseBase), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorDetailsResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorDetailsResponse), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ResponseBase>> ContactUs([FromBody] ContactUsCommand contactUsCommand,
                                                            CancellationToken cancellationToken)
        => await mediator.Send(contactUsCommand, cancellationToken) switch
        {
            { IsSuccess: true, Value: ResponseBase viewModel } => Ok(viewModel),
            _ => CreateBadRequestObjectResult()
        };
}