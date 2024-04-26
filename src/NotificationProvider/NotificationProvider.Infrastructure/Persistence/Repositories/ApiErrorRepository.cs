using NotificationProvider.Application.Interfaces.Persistence.Repositories;
using NotificationProvider.Domain.Entities;

namespace NotificationProvider.Infrastructure.Persistence.Repositories;

internal class ApiErrorRepository : IApiErrorRepository
{
    public Task CreateAsync(ApiError apiError,
                            CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}