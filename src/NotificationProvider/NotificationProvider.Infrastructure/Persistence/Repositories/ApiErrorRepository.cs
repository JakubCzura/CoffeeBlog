using NotificationProvider.Application.Interfaces.Persistence.Repositories;
using NotificationProvider.Domain.Entities;
using NotificationProvider.Infrastructure.Persistence.DatabaseContext;

namespace NotificationProvider.Infrastructure.Persistence.Repositories;

internal class ApiErrorRepository(NotificationProviderDbContext notificationProviderDbContext) 
    : DbEntityBaseRepository<ApiError>(notificationProviderDbContext), IApiErrorRepository
{
}