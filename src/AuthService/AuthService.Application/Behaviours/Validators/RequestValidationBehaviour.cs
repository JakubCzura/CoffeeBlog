﻿using FluentValidation;
using MediatR;

namespace AuthService.Application.Behaviours.Validators;

/// <summary>
/// Request validation behavior to validate commands and queries for CQRS pattern using FluentValidation.
/// </summary>
/// <typeparam name="TRequest">Command or query for CQRS.</typeparam>
/// <typeparam name="TResponse">Response from command or query.</typeparam>
/// <param name="_validators">FluentValidation validators to validate commands and queries.</param>
public sealed class RequestValidationBehavior<TRequest, TResponse>(IEnumerable<IValidator<TRequest>> _validators) 
    : IPipelineBehavior<TRequest, TResponse> where TRequest : class, IRequest<TResponse>
{
    private readonly IEnumerable<IValidator<TRequest>> _validators = _validators;

    /// <summary>
    /// Handles CQRS command or query request validation.
    /// </summary>
    /// <param name="request">Command or query.</param>
    /// <param name="next">Delegate to perform action.</param>
    /// <param name="cancellationToken">Token to cancel asynchronous operation.</param>
    /// <returns>Response from command or query.</returns>
    /// <exception cref="ValidationException">When validation fails.</exception>
    public async Task<TResponse> Handle(TRequest request,
                                        RequestHandlerDelegate<TResponse> next,
                                        CancellationToken cancellationToken)
    {
        if (!_validators.Any())
        {
            return await next();
        }

        var context = new ValidationContext<TRequest>(request);

        string errorsMessages = string.Join(";", _validators.Select(x => x.Validate(context))
                                                            .SelectMany(x => x.Errors)
                                                            .Select(x => x.ErrorMessage));

        if (!string.IsNullOrWhiteSpace(errorsMessages))
        {
            throw new ValidationException(errorsMessages);
        }

        return await next();
    }
}