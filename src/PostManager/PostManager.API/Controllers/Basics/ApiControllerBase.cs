using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PostManager.Domain.Constants;

namespace PostManager.API.Controllers.Basics;

/// <summary>
/// Base controller for all API controllers in the application.
/// </summary>
/// <param name="_mediator">Mediator to handle commands and queries using CQRS pattern.</param>
[ApiController]
[Route(RouteConstants.ApiController)]
[Produces(ContentTypeConstants.ApplicationJson)]
[Authorize]
public class ApiControllerBase(IMediator _mediator) : ControllerBase
{
    /// <summary>
    /// Mediator to handle commands and queries using CQRS pattern.
    /// </summary>
    protected readonly IMediator Mediator = _mediator;
}