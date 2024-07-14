using StatisticsCollector.Application.Interfaces.Persistence.Repositories;
using StatisticsCollector.Domain.Entities;
using StatisticsCollector.Infrastructure.Persistence.DatabaseContext;

namespace StatisticsCollector.Infrastructure.Persistence.Repositories;

/// <summary>
/// Repository to perform database operations related to <see cref="UsersDiagnostics"/>.
/// </summary>
/// <param name="_statisticsCollectorDbContext">Database context.</param>
internal class UsersDiagnosticsRepository(StatisticsCollectorDbContext _statisticsCollectorDbContext)
    : DbEntityBaseRepository<UsersDiagnostics>(_statisticsCollectorDbContext), IUsersDiagnosticsRepository
{
}