using NotificationProvider.Domain.Entities;

namespace NotificationProvider.Application.Interfaces.Persistence.Repositories;

public interface IApiErrorRepository
{
    Task CreateAsync(ApiError apiError,
                     CancellationToken cancellationToken);
}