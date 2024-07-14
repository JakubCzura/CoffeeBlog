using NotificationProvider.Application.Interfaces.Persistence.Repositories;
using NotificationProvider.Domain.Entities;
using NotificationProvider.Infrastructure.Persistence.DatabaseContext;

namespace NotificationProvider.Infrastructure.Persistence.Repositories;

/// <summary>
/// Repository to perform database operations related to <see cref="EventConsumerDetail"/>.
/// </summary>
/// <param name="_notificationProviderDbContext">Database context.</param>
internal class EventConsumerDetailRepository(NotificationProviderDbContext _notificationProviderDbContext)
    : DbEntityBaseRepository<EventConsumerDetail>(_notificationProviderDbContext), IEventConsumerDetailRepository
{
}