﻿using CoffeeBlog.Domain.Entities;

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
            _logger.LogError(exception, "Exception caught by exception middleware");

            ApiError error = new(exception.ToString(),
                                       exception.Message,
                                       "Exception caught by exception middleware",
                                       DateTime.UtcNow);

            //Write data to database
        }
    }
}