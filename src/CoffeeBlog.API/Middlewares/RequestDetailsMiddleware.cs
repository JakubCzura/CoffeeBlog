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
        string? responseBody = null;

        context.Request.EnableBuffering();

        string? requestBody = await ReadBodyAsString(context.Request.ContentType,
                                                     context.Request.Body);

        Stream originalBodyStream = context.Response.Body;
        await using (MemoryStream responseBodyStream = new())
        {
            context.Response.Body = responseBodyStream;

            await next.Invoke(context);

            responseBody = await ReadBodyAsString(context.Request.ContentType,
                                                  context.Response.Body);

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

    private static bool IsJson(string? contentType)
        => !string.IsNullOrWhiteSpace(contentType) 
           && contentType.Equals(Constants.ContentType.ApplicationJson, StringComparison.OrdinalIgnoreCase);

    private static async Task<string?> ReadBodyAsString(string? contentType,
                                                        Stream body)
    {
        string? bodyAsString = null;
        if (IsJson(contentType))
        {
            body.Seek(0, SeekOrigin.Begin);
            bodyAsString = await body.ReadAsStringAsync();
        }
        body.Seek(0, SeekOrigin.Begin);
        return bodyAsString;
    }
}