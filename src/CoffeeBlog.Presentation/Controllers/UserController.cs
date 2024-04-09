using Asp.Versioning;
using AuthService.Presentation.Controllers.Basics;
using AuthService.Presentation.ExtensionMethods.Versioning;
using MediatR;

namespace AuthService.Presentation.Controllers;

[ApiVersion(ApiVersioningInfo.Version_1_0)]
public class UserController(IMediator _mediator) : ApiControllerBase(_mediator)
{
}