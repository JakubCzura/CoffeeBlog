using CoffeeBlog.Domain.Constants;
using Microsoft.AspNetCore.Mvc;

namespace CoffeeBlog.API.Controllers.Base;

[ApiController]
[Route(Constants.Route.ApiController)]
public class APIControllerBase : ControllerBase
{
}
