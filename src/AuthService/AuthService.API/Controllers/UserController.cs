using Asp.Versioning;
using AuthService.API.Controllers.Basics;
using AuthService.API.ExtensionMethods.Versioning;
using MediatR;

namespace AuthService.API.Controllers;

[ApiVersion(ApiVersioningInfo.Version_1_0)]
public class UserController(IMediator _mediator) : ApiControllerBase(_mediator)
{
}