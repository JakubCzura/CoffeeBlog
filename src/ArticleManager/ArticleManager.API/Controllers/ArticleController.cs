using ArticleManager.API.Controllers.Basics;
using ArticleManager.API.ExtensionMethods.Versioning;
using Asp.Versioning;
using MediatR;

namespace ArticleManager.API.Controllers;

/// <summary>
/// Controller to manage article entity.
/// </summary>
/// <param name="_mediator">Mediator to handle requests' commands and queries.</param>
[ApiVersion(ApiVersioningInfo.Version_1_0)]
public class ArticleController(IMediator _mediator) : ApiControllerBase(_mediator)
{
}