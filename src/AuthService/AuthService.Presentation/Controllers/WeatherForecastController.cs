using Asp.Versioning;
using AuthService.Domain.ViewModels;
using AuthService.Presentation.Controllers.Basics;
using AuthService.Presentation.ExtensionMethods.Versioning;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AuthService.Presentation.Controllers;

[AllowAnonymous]
[ApiVersion(ApiVersioningInfo.Version_1_0)]
public class WeatherForecastController(ILogger<WeatherForecastController> logger, IMediator _mediator) : ApiControllerBase(_mediator)
{
    private static readonly string[] Summaries =
    [
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    ];

    private readonly ILogger<WeatherForecastController> _logger = logger;

    [HttpGet("All")]
    public IEnumerable<WeatherForecast> GetAll()
    {
        return Enumerable.Range(1, 5).Select(index => new WeatherForecast
        {
            Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        })
        .ToArray();
    }
}