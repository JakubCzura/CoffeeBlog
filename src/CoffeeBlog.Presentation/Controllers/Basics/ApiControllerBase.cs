using CoffeeBlog.Domain.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CoffeeBlog.Presentation.Controllers.Basics;

[ApiController]
[Route(RouteConstants.ApiController)]
[Produces(ContentTypeConstants.ApplicationJson)]
[Authorize]
public class ApiControllerBase : ControllerBase
{
}
