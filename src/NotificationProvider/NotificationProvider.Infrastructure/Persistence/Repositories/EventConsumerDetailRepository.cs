﻿using NotificationProvider.Application.Interfaces.Persistence.Repositories;
using NotificationProvider.Domain.Entities;
using NotificationProvider.Infrastructure.Persistence.DatabaseContext;

namespace NotificationProvider.Infrastructure.Persistence.Repositories;

internal class EventConsumerDetailRepository(NotificationProviderDbContext notificationProviderDbContext)
    : DbEntityBaseRepository<EventConsumerDetail>(notificationProviderDbContext), IEventConsumerDetailRepository
{
}