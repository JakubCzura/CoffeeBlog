using CoffeeBlog.Domain.Constants;
using CoffeeBlog.Domain.Entities;
using CoffeeBlog.Domain.Exceptions;
using CoffeeBlog.Domain.ViewModels.Errors;
using System.Net;

namespace CoffeeBlog.Presentation.Middlewares;

/// <summary>
/// Middleware to handle request's exception. It logs the exception and returns a response with exception's details.
/// </summary>
/// <param name="_logger">Logger to log exceptions.</param>
public class ExceptionMiddleware(ILogger<ExceptionMiddleware> _logger) : IMiddleware
{
    private readonly ILogger<ExceptionMiddleware> _logger = _logger;

    /// <summary>
    /// Handles request's exception.
    /// </summary>
    /// <param name="context">Request's context.</param>
    /// <param name="next">Delegate to process request.</param>
    /// <returns>Task.</returns>
    public async Task InvokeAsync(HttpContext context,
                                  RequestDelegate next)
    {
        try
        {
            await next.Invoke(context);
        }
        catch (Exception exception)
        {
            //TODO: Write data to database in HandleExceptionAsync()
            ApiError error = new(exception.ToString(),
                                 exception.Message,
                                 "Exception caught by exception middleware",
                                 DateTime.UtcNow);

            await HandleExceptionAsync(context, exception);
        }
    }

    private async Task HandleExceptionAsync(HttpContext httpContext,
                                            Exception exception)
    {
        _logger.LogError(exception, "Exception caught by exception middleware");

        ErrorDetailsViewModel errorDetailsViewModel = exception switch
        {
            NullEntityException => CreateErrorDetailsResponse(httpContext, HttpStatusCode.BadRequest, exception.Message),
            _ => CreateErrorDetailsResponse(httpContext, HttpStatusCode.InternalServerError, "Internal server exception.")
        };

        await httpContext.Response.WriteAsync(errorDetailsViewModel.ToString(), httpContext.RequestAborted);
    }

    private static ErrorDetailsViewModel CreateErrorDetailsResponse(HttpContext httpContext,
                                                                    HttpStatusCode statusCode,
                                                                    string responseMessage)
    {
        httpContext.Response.ContentType = ContentTypeConstants.ApplicationJson;
        httpContext.Response.StatusCode = (int)statusCode;

        return new ErrorDetailsViewModel(httpContext.Response.StatusCode, responseMessage);
    }
}