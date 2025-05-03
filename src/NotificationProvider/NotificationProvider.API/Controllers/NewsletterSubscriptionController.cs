using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NotificationProvider.API.Controllers.Basics;
using NotificationProvider.API.ExtensionMethods.Versioning;
using Shared.Application.Common.Responses.Basics;
using Shared.Application.Common.Responses.Errors;
using Shared.Application.NotificationProvider.Commands.NewsletterSubscriptions.CancelNewsletterSubscription;
using Shared.Application.NotificationProvider.Commands.NewsletterSubscriptions.ConfirmNewsletterSubscription;
using Shared.Application.NotificationProvider.Commands.NewsletterSubscriptions.SubscribeNewsletter;

namespace NotificationProvider.API.Controllers;

/// <summary>
/// Controller to manage newsletter subscriptions.
/// </summary>
/// <param name="mediator">Mediator to handle command</param>
[ApiVersion(ApiVersioningInfo.Version_1_0)]
public class NewsletterSubscriptionController(IMediator mediator) : ApiControllerBase
{
    /// <summary>
    /// Endpoint to subscribe user to newsletter.
    /// </summary>
    /// <param name="subscribeNewsletterCommand">Details to subscribe user to newsletter.</param>
    /// <param name="cancellationToken">Token to cancel asynchronous operation.</param>
    /// <returns>Information about subscribing user to newsletter.</returns>
    [AllowAnonymous]
    [HttpPost("subscribe")]
    [ProducesResponseType(typeof(ResponseBase), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorDetailsResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorDetailsResponse), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ResponseBase>> Subscribe([FromBody] SubscribeNewsletterCommand subscribeNewsletterCommand,
                                                            CancellationToken cancellationToken)
        => await mediator.Send(subscribeNewsletterCommand, cancellationToken) switch
        {
            { IsSuccess: true, Value: ResponseBase viewModel } => Ok(viewModel),
            _ => CreateBadRequestObjectResult()
        };

    /// <summary>
    /// Endpoint to confirm subscription to newsletter.
    /// </summary>
    /// <param name="confirmNewsletterSubscriptionCommand">Details to confirm subscription to newsletter.</param>
    /// <param name="cancellationToken">Token to cancel asynchronous operation.</param>
    /// <returns>Information about confirming subscription to newsletter.</returns>
    [AllowAnonymous]
    [HttpPost("confirm")]
    [ProducesResponseType(typeof(ResponseBase), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorDetailsResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorDetailsResponse), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ResponseBase>> Confirm([FromBody] ConfirmNewsletterSubscriptionCommand confirmNewsletterSubscriptionCommand,
                                                          CancellationToken cancellationToken)
        => await mediator.Send(confirmNewsletterSubscriptionCommand, cancellationToken) switch
        {
            { IsSuccess: true, Value: ResponseBase viewModel } => Ok(viewModel),
            _ => CreateBadRequestObjectResult()
        };

    /// <summary>
    /// Endpoint to cancel subscription to newsletter.
    /// </summary>
    /// <param name="cancelNewsletterSubscriptionCommand">Details to cancel subscription to newsletter.</param>
    /// <param name="cancellationToken">Token to cancel asynchronous operation.</param>
    /// <returns>Information about cancelling subscription to newsletter.</returns>
    [AllowAnonymous]
    [HttpPost("cancel")]
    [ProducesResponseType(typeof(ResponseBase), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorDetailsResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorDetailsResponse), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ResponseBase>> Cancel([FromBody] CancelNewsletterSubscriptionCommand cancelNewsletterSubscriptionCommand,
                                                         CancellationToken cancellationToken)
        => await mediator.Send(cancelNewsletterSubscriptionCommand, cancellationToken) switch
        {
            { IsSuccess: true, Value: ResponseBase viewModel } => Ok(viewModel),
            _ => CreateBadRequestObjectResult()
        };
}