using CoffeeBlog.Domain.Constants;
using CoffeeBlog.Domain.Entities;
using CoffeeBlog.Domain.Exceptions;
using CoffeeBlog.Domain.ViewModels.Errors;
using System.Net;

namespace CoffeeBlog.Presentation.Middlewares;

public class ExceptionMiddleware(ILogger<ExceptionMiddleware> logger) : IMiddleware
{
    private readonly ILogger<ExceptionMiddleware> _logger = logger;

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

        httpContext.Response.ContentType = ContentTypeConstants.ApplicationJson;

        ErrorDetailsViewModel errorDetailsViewModel = exception switch
        {
            NullEntityException nullEntityException => CreateErrorDetailsResponse(httpContext, HttpStatusCode.BadRequest, exception.Message),
            _ => CreateErrorDetailsResponse(httpContext, HttpStatusCode.InternalServerError, "Internal server exception.")
        };

        await httpContext.Response.WriteAsync(errorDetailsViewModel.ToString(), httpContext.RequestAborted);
    }

    private static ErrorDetailsViewModel CreateErrorDetailsResponse(HttpContext httpContext,
                                                                    HttpStatusCode statusCode,
                                                                    string responseMessage)
    {
        httpContext.Response.StatusCode = (int)statusCode;

        return new ErrorDetailsViewModel(httpContext.Response.StatusCode, responseMessage);
    }
}