using CoffeeBlog.Domain.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CoffeeBlog.API.Controllers.ControllersBase;

[ApiController]
[Route(Constants.Route.ApiController)]
[Authorize]
public class ApiControllerBase : ControllerBase
{
}