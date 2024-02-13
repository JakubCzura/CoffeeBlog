using CoffeeBlog.API.ExtensionMethods;
using CoffeeBlog.Domain.Entities.Request;
using System.Diagnostics;

namespace CoffeeBlog.API.Middlewares;

public class RequestDetailsMiddleware(RequestDelegate next)
{
    private readonly RequestDelegate _next = next;

    public async Task InvokeAsync(HttpContext httpContext)
    {
        Stopwatch stopwatch = Stopwatch.StartNew();

        httpContext.Request.EnableBuffering();
        httpContext.Request.Body.Position = 0;
        string requestBody = await httpContext.Request.Body.ReadAsStringAsync();
        httpContext.Request.Body.Position = 0;

        string responseBody = string.Empty;
        Stream originalBodyStream = httpContext.Response.Body;
        using (MemoryStream responseBodyStream = new())
        {
            httpContext.Response.Body = responseBodyStream;

            await _next(httpContext);

            httpContext.Response.Body.Seek(0, SeekOrigin.Begin);
            responseBody = await httpContext.Response.Body.ReadAsStringAsync();
            httpContext.Response.Body.Seek(0, SeekOrigin.Begin);
            await responseBodyStream.CopyToAsync(originalBodyStream);
        }

        stopwatch.Stop();
        RequestDetail requestDetail = new(httpContext.GetRouteData().Values["controller"]?.ToString() ?? string.Empty,
                                          httpContext.Request.Path,
                                          httpContext.Request.Method,
                                          httpContext.Response.StatusCode,
                                          requestBody,
                                          httpContext.Request.ContentType,
                                          responseBody,
                                          httpContext.Response.ContentType,
                                          stopwatch.ElapsedMilliseconds,
                                          DateTime.UtcNow);

        //Write data to database
    }
}