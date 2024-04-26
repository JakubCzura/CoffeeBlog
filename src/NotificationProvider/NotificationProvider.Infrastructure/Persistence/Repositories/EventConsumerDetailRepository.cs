using NotificationProvider.Application.Interfaces.Persistence.Repositories;
using NotificationProvider.Domain.Entities;

namespace NotificationProvider.Infrastructure.Persistence.Repositories;

internal class EventConsumerDetailRepository : IEventConsumerDetailRepository
{
    public Task CreateAsync(EventConsumerDetail eventConsumerDetail,
                            CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}