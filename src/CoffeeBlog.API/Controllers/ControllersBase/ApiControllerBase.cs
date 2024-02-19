using CoffeeBlog.Domain.Constants;
using Microsoft.AspNetCore.Mvc;

namespace CoffeeBlog.API.Controllers.ControllersBase;

[ApiController]
[Route(Constants.Route.ApiController)]
public class ApiControllerBase : ControllerBase
{
}
