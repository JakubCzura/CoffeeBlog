using ArticleManager.API.ExtensionMethods.Request;
using ArticleManager.Application.Commands.RequestDetails.CreateRequestDetail;
using ArticleManager.Domain.Constants;
using MediatR;
using System.Diagnostics;
using System.Security.Claims;

namespace ArticleManager.API.Middlewares;

/// <summary>
/// Middleware to measure request's details. It writes data like request's time, path, method etc. to database.
/// </summary>
/// <param name="_logger">Logger to log exceptions.</param>
/// <param name="_mediator">Mediator to handle command to save request's details in database.</param>
public class RequestDetailsMiddleware(ILogger<RequestDetailsMiddleware> _logger,
                                      IMediator _mediator) 
    : IMiddleware
{
    private readonly ILogger<RequestDetailsMiddleware> _logger = _logger;
    private readonly IMediator _mediator = _mediator;

    /// <summary>
    /// Measures request's details and writes them to database.
    /// <para>Important: this middleware should not throw any exception.
    /// Requests' exceptions are handled by <see cref="ExceptionMiddleware"/> and proper response is returned informing that request failed.
    /// If an exception occurs in this middleware it should be checked and repaired as it might be a problem with database or processing requests.</para>
    /// </summary>
    /// <param name="httpContext">Request's context.</param>
    /// <param name="next">Delegate to process request.</param>
    /// <returns><see cref="Task"/>.</returns>
    public async Task InvokeAsync(HttpContext httpContext,
                                  RequestDelegate next)
    {
        try
        {
            Stopwatch stopwatch = Stopwatch.StartNew();
            string? responseBody = null;

            httpContext.Request.EnableBuffering();

            string? requestBody = await ReadBodyAsString(httpContext.Request.ContentType,
                                                         httpContext.Request.Body);

            Stream originalBodyStream = httpContext.Response.Body;
            await using (MemoryStream responseBodyStream = new())
            {
                httpContext.Response.Body = responseBodyStream;

                await next.Invoke(httpContext);

                responseBody = await ReadBodyAsString(httpContext.Request.ContentType,
                                                      httpContext.Response.Body);

                await responseBodyStream.CopyToAsync(originalBodyStream);
            }

            string? userId = httpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

            stopwatch.Stop();

            //Send data to StatisticsCollector microservice
            CreateRequestDetailCommand createRequestDetailCommand = new(
                httpContext.GetRouteData().Values["controller"]?.ToString() ?? string.Empty,
                httpContext.Request.Path,
                httpContext.Request.Method,
                httpContext.Response.StatusCode,
                requestBody,
                httpContext.Request.ContentType,
                responseBody,
                httpContext.Response.ContentType,
                stopwatch.ElapsedMilliseconds,
                userId is not null ? int.Parse(userId) : null
            );
            await _mediator.Send(createRequestDetailCommand, httpContext.RequestAborted);
        }
        catch (Exception exception)
        {
            _logger.LogCritical(exception, $"{nameof(RequestDetailsMiddleware)}: Exception while sending request's data to StatisticsCollector microservice.");
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