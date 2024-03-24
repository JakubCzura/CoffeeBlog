using Asp.Versioning;
using CoffeeBlog.Presentation.Controllers.Basics;
using CoffeeBlog.Presentation.ExtensionMethods.Versioning;
using MediatR;

namespace CoffeeBlog.Presentation.Controllers;

[ApiVersion(ApiVersioningInfo.Version_1_0)]
public class UserController(IMediator _mediator) : ApiControllerBase(_mediator)
{
}