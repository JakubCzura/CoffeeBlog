using CoffeeBlog.API.ExtensionMethods;
using CoffeeBlog.Domain.Constants;
using CoffeeBlog.Domain.Entities;
using System.Diagnostics;

namespace CoffeeBlog.API.Middlewares;

public class RequestDetailsMiddleware : IMiddleware
{
    public async Task InvokeAsync(HttpContext context,
                                  RequestDelegate next)
    {
        Stopwatch stopwatch = Stopwatch.StartNew();

        string? requestBody = null;
        string? responseBody = null;

        if (!string.IsNullOrWhiteSpace(context.Request.ContentType) && context.Request.ContentType.Equals(Constants.ContentType.ApplicationJson, StringComparison.OrdinalIgnoreCase))
        {
            context.Request.EnableBuffering();
            context.Request.Body.Position = 0;
            requestBody = await context.Request.Body.ReadAsStringAsync();
            context.Request.Body.Position = 0;
        }

        Stream originalBodyStream = context.Response.Body;
        await using (MemoryStream responseBodyStream = new())
        {
            context.Response.Body = responseBodyStream;

            await next.Invoke(context);

            if (!string.IsNullOrWhiteSpace(context.Response.ContentType) && context.Response.ContentType.Equals(Constants.ContentType.ApplicationJson, StringComparison.OrdinalIgnoreCase))
            {
                context.Response.Body.Seek(0, SeekOrigin.Begin);
                responseBody = await context.Response.Body.ReadAsStringAsync();
                context.Response.Body.Seek(0, SeekOrigin.Begin);
            }

            await responseBodyStream.CopyToAsync(originalBodyStream);
        }

        stopwatch.Stop();
        RequestDetail requestDetail = new(context.GetRouteData().Values["controller"]?.ToString() ?? string.Empty,
                                          context.Request.Path,
                                          context.Request.Method,
                                          context.Response.StatusCode,
                                          requestBody,
                                          context.Request.ContentType,
                                          responseBody,
                                          context.Response.ContentType,
                                          stopwatch.ElapsedMilliseconds,
                                          DateTime.UtcNow);

        //Write data to database
    }
}