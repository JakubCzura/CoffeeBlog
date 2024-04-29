﻿using NotificationProvider.Application.Interfaces.Persistence.Repositories;
using NotificationProvider.Domain.Entities;
using NotificationProvider.Infrastructure.Persistence.DatabaseContext;

namespace NotificationProvider.Infrastructure.Persistence.Repositories;

internal class EmailMessageDetailRepository(NotificationProviderDbContext notificationProviderDbContext)
    : DbEntityBaseRepository<EmailMessageDetail>(notificationProviderDbContext), IEmailMessageDetailRepository
{
}