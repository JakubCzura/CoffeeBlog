using CoffeeBlog.Domain.Entities.Errors;

namespace CoffeeBlog.API.Middlewares;

public class ExceptionMiddleware(RequestDelegate next,
                                 ILogger<ExceptionMiddleware> logger)
{
    private readonly RequestDelegate _next = next;
    private readonly ILogger<ExceptionMiddleware> _logger = logger;

    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await _next(httpContext);
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, "Exception caught by exception middleware");

            APIError error = new(exception.ToString(),
                              exception.Message,
                              "Exception caught by exception middleware",
                              DateTime.UtcNow);

            //Write data to database
        }
    }
}