namespace CoffeeBlog.API.Middlewares;

public class RequestDetailsMiddleware(RequestDelegate next)
{
    private readonly RequestDelegate _next = next;

    public async Task InvokeAsync(HttpContext httpContext)
    {
        await _next(httpContext);
    }
}