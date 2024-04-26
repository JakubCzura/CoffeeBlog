using NotificationProvider.Domain.Entities;

namespace NotificationProvider.Application.Interfaces.Persistence.Repositories;

public interface IEventConsumerDetailRepository
{
    Task CreateAsync(EventConsumerDetail eventConsumerDetail,
                     CancellationToken cancellationToken);
}