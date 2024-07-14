using StatisticsCollector.Application.Interfaces.Persistence.Repositories;
using StatisticsCollector.Domain.Entities;
using StatisticsCollector.Infrastructure.Persistence.DatabaseContext;

namespace StatisticsCollector.Infrastructure.Persistence.Repositories;

/// <summary>
/// Repository to perform database operations related to <see cref="ApiError"/>.
/// </summary>
/// <param name="_statisticsCollectorDbContext">Database context.</param>
internal class ApiErrorRepository(StatisticsCollectorDbContext _statisticsCollectorDbContext)
    : DbEntityBaseRepository<ApiError>(_statisticsCollectorDbContext), IApiErrorRepository
{
}