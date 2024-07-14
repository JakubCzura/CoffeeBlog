using NotificationProvider.Application.Interfaces.Persistence.Repositories;
using NotificationProvider.Domain.Entities;
using NotificationProvider.Infrastructure.Persistence.DatabaseContext;

namespace NotificationProvider.Infrastructure.Persistence.Repositories;

/// <summary>
/// Repository to perform database operations related to <see cref="ApiError"/>.
/// </summary>
/// <param name="_notificationProviderDbContext">Database context.</param>
internal class ApiErrorRepository(NotificationProviderDbContext _notificationProviderDbContext)
    : DbEntityBaseRepository<ApiError>(_notificationProviderDbContext), IApiErrorRepository
{
}