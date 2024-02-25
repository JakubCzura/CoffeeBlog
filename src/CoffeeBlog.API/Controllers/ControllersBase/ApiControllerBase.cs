using CoffeeBlog.Domain.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CoffeeBlog.API.Controllers.ControllersBase;

[ApiController]
[Route(Constants.Route.ApiController)]
[Produces(Constants.ContentType.ApplicationJson)]
[Authorize]
public class ApiControllerBase : ControllerBase
{
}