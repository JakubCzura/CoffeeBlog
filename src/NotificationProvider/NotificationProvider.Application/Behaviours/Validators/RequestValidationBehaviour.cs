using FluentValidation;
using FluentValidation.Results;
using MediatR;

namespace NotificationProvider.Application.Behaviours.Validators;

/// <summary>
/// Request validation behavior to validate commands and queries for CQRS pattern using FluentValidation.
/// </summary>
/// <typeparam name="TRequest">Command or query for CQRS.</typeparam>
/// <typeparam name="TResponse">Response from command or query.</typeparam>
/// <param name="validators">FluentValidation validators to validate commands and queries.</param>
public sealed class RequestValidationBehavior<TRequest, TResponse>(IEnumerable<IValidator<TRequest>> validators)
    : IPipelineBehavior<TRequest, TResponse> where TRequest : class, IRequest<TResponse>
{
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
        if (!validators.Any())
        {
            return await next(cancellationToken);
        }

        ValidationContext<TRequest> context = new(request);

        IEnumerable<ValidationFailure> errors = validators.Select(v => v.Validate(context))
                                                          .SelectMany(result => result.Errors);

        if (errors.Any())
        {
            throw new ValidationException(errors);
        }

        return await next(cancellationToken);
    }
}