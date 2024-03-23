using CoffeeBlog.Domain.Constants;
using CoffeeBlog.Domain.Entities;
using CoffeeBlog.Presentation.ExtensionMethods.Request;
using System.Diagnostics;
using System.Security.Claims;

namespace CoffeeBlog.Presentation.Middlewares;

/// <summary>
/// Middleware to measure request's details. It writes data like request's time, path, method etc. to database.
/// </summary>
/// <param name="_logger">Logger to log exceptions.</param>
public class RequestDetailsMiddleware(ILogger<RequestDetailsMiddleware> _logger) : IMiddleware
{
    private readonly ILogger<RequestDetailsMiddleware> _logger = _logger;

    /// <summary>
    /// Measures request's details and writes them to database.
    /// <para>Important: this middleware should not throw any exception. 
    /// Requests' exceptions are handled by <see cref="ExceptionMiddleware"/> and proper response is returned informing that request failed.
    /// If an exception occurs in this middleware it should be checked and repaired as it might be a problem with database or processing requests.</para>
    /// </summary>
    /// <param name="context">Request's context.</param>
    /// <param name="next">Delegate to process request.</param>
    /// <returns>Task.</returns>
    public async Task InvokeAsync(HttpContext context,
                                  RequestDelegate next)
    {
        try
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

            string? userId = context.User.FindFirstValue(ClaimTypes.NameIdentifier);

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
                                                    userId is not null ? int.Parse(userId) : null);
            //Write data to database
        }
        catch (Exception exception)
        {
            _logger.LogCritical(exception, "Exception while saving request's data.");
        }
    }

    private static bool IsJson(string? contentType)
        => !string.IsNullOrWhiteSpace(contentType)
           && contentType.Equals(ContentTypeConstants.ApplicationJson, StringComparison.OrdinalIgnoreCase);

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