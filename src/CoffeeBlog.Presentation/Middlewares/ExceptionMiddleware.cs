using CoffeeBlog.Domain.Commands.ApiErrors;
using CoffeeBlog.Domain.Constants;
using CoffeeBlog.Domain.Exceptions;
using CoffeeBlog.Domain.ViewModels.Errors;
using MediatR;
using System.Net;

namespace CoffeeBlog.Presentation.Middlewares;

/// <summary>
/// Middleware to handle request's exception. It logs the exception and returns a response with exception's details.
/// </summary>
/// <param name="_logger">Logger to log exceptions.</param>
/// <param name="_mediator">Mediator to handle command to save API error in database.</param>
public class ExceptionMiddleware(ILogger<ExceptionMiddleware> _logger,
                                 IMediator _mediator) : IMiddleware
{
    private readonly ILogger<ExceptionMiddleware> _logger = _logger;
    private readonly IMediator _mediator = _mediator;

    /// <summary>
    /// Handles request's exception.
    /// </summary>
    /// <param name="httpContext">Request's context.</param>
    /// <param name="next">Delegate to process request.</param>
    /// <returns>Task.</returns>
    public async Task InvokeAsync(HttpContext httpContext,
                                  RequestDelegate next)
    {
        try
        {
            await next.Invoke(httpContext);
        }
        catch (Exception exception)
        {
            await HandleExceptionAsync(httpContext, exception);
        }
    }

    private async Task HandleExceptionAsync(HttpContext httpContext,
                                            Exception exception)
    {
        _logger.LogError(exception, "Exception caught by exception middleware");

        try
        {
            CreateApiErrorCommand createApiErrorCommand = new()
            {
                Name = exception.GetType().Name,
                Exception = exception.ToString(),
                Message = exception.Message,
                Description = "Exception caught by exception middleware"
            };
            await _mediator.Send(createApiErrorCommand, httpContext.RequestAborted);
        }
        catch (Exception)
        {
            _logger.LogCritical(exception, $"{nameof(ExceptionMiddleware)}: Exception while saving API exception's data to database.");
        }

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