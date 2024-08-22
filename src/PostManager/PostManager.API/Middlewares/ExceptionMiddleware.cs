using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using PostManager.Application.Commands.ApiErrors.CreateApiError;
using PostManager.Domain.Constants;
using PostManager.Domain.Exceptions;
using PostManager.Domain.ViewModels.Errors;
using System.Diagnostics;
using System.Net;

namespace PostManager.API.Middlewares;

/// <summary>
/// Middleware to handle request's exception. It logs the exception and returns a response with exception's details.
/// </summary>
/// <param name="logger">Logger to log exceptions.</param>
/// <param name="mediator">Mediator to handle command to save API error in database.</param>
public class ExceptionMiddleware(ILogger<ExceptionMiddleware> logger,
                                 IMediator mediator) : IMiddleware
{
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
        logger.LogError(exception, "Exception caught by exception middleware");

        try
        {
            CreateApiErrorCommand createApiErrorCommand = new(
                exception.GetType().Name,
                exception.ToString(),
                exception.Message,
                "Exception caught by exception middleware"
            );
            await mediator.Send(createApiErrorCommand, httpContext.RequestAborted);
        }
        catch (Exception)
        {
            logger.LogCritical(exception, $"{nameof(ExceptionMiddleware)}: Exception while saving API exception's data to database.");
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