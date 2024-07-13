using Asp.Versioning;
using MediatR;
using PostManager.API.Controllers.Basics;
using PostManager.API.ExtensionMethods.Versioning;

namespace PostManager.API.Controllers;

/// <summary>
/// Controller to manage post entity.
/// </summary>
/// <param name="_mediator">Mediator to handle commands and queries using CQRS pattern.</param>
[ApiVersion(ApiVersioningInfo.Version_1_0)]
public class PostController(IMediator _mediator) : ApiControllerBase(_mediator)
{
}