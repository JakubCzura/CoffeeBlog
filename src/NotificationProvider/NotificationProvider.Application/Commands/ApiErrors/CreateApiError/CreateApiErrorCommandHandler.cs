using AutoMapper;
using MediatR;
using NotificationProvider.Application.Interfaces.Persistence.Repositories;
using NotificationProvider.Domain.Entities;

namespace NotificationProvider.Application.Commands.ApiErrors.CreateApiError;

/// <summary>
/// Command handler to create new API error and save it to database. It's related to <see cref="CreateApiErrorCommand"/>.
/// </summary>
/// <param name="apiErrorRepository">Interface to perform API error's operations in database.</param>
/// <param name="mapper">AutoMapper to map classes.</param>
public class CreateApiErrorCommandHandler(IApiErrorRepository apiErrorRepository,
                                          IMapper mapper)
    : IRequestHandler<CreateApiErrorCommand, Unit>
{
    /// <summary>
    /// Handles request to create new API error and save it to database.
    /// </summary>
    /// <param name="request">Request command with details to create API error.</param>
    /// <param name="cancellationToken">Token to cancel asynchronous operation.</param>
    /// <returns><see cref="Unit.Value"/></returns>
    public async Task<Unit> Handle(CreateApiErrorCommand request,
                                   CancellationToken cancellationToken)
    {
        ApiError apiError = mapper.Map<ApiError>(request);
        await apiErrorRepository.CreateAsync(apiError, cancellationToken);

        return Unit.Value;
    }
}