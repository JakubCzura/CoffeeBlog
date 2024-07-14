using ArticleManager.Application.Commands.ApiErrors.CreateApiError;
using ArticleManager.Domain.Constants;
using ArticleManager.Domain.Exceptions;
using ArticleManager.Domain.ViewModels.Errors;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Net;

namespace ArticleManager.API.Middlewares;

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
    /// <returns><see cref="Task"/>.</returns>
    public async Task InvokeAsync(HttpContext httpContext,
                                  RequestDelegate next)
    {
        try
        {
            await next.Invoke(httpContext);
        }
        catch (ValidationException exception)
        {
            await HandleValidationExceptionAsync(httpContext, exception);
        }
        catch (Exception exception)
        {
            await HandleExceptionAsync(httpContext, exception);
        }
    }

    private static async Task HandleValidationExceptionAsync(HttpContext httpContext, ValidationException exception)
    {
        Dictionary<string, string[]> validationErrors = exception.Errors.GroupBy(x => x.PropertyName, x => x.ErrorMessage,
                                                                        (propertyName, errorMessages) => new
                                                                        {
                                                                            Key = propertyName,
                                                                            Values = errorMessages.Distinct().ToArray()
                                                                        })
                                                                        .ToDictionary(x => x.Key, x => x.Values);
        ValidationProblemDetails validationProblemDetails = new(validationErrors)
        {
            Type = ValidationProblemDetailsConstants.BadRequestType,
            Status = StatusCodes.Status400BadRequest
        };
        validationProblemDetails.Extensions.Add(ValidationProblemDetailsConstants.TraceId, Activity.Current?.Id ?? httpContext.TraceIdentifier);

        httpContext.Response.ContentType = ContentTypeConstants.ApplicationJson;
        httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
        await httpContext.Response.WriteAsJsonAsync(validationProblemDetails, httpContext.RequestAborted);
    }

    private async Task HandleExceptionAsync(HttpContext httpContext,
                                            Exception exception)
    {
        _logger.LogError(exception, "Exception caught by exception middleware");

        try
        {
            CreateApiErrorCommand createApiErrorCommand = new(
                exception.GetType().Name,
                exception.ToString(),
                exception.Message,
                "Exception caught by exception middleware"
            );
            await _mediator.Send(createApiErrorCommand, httpContext.RequestAborted);
        }
        catch (Exception)
        {
            _logger.LogCritical(exception, $"{nameof(ExceptionMiddleware)}: Exception while saving API exception's data to database.");
        }

        ErrorDetailsViewModel errorDetailsViewModel = exception switch
        {
            ValidationException validationException => CreateErrorDetailsResponse(httpContext, HttpStatusCode.BadRequest, validationException.Message),
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