using StatisticsCollector.Application.Interfaces.Persistence.Repositories;
using StatisticsCollector.Domain.Entities;
using StatisticsCollector.Infrastructure.Persistence.DatabaseContext;

namespace StatisticsCollector.Infrastructure.Persistence.Repositories;

internal class UsersDiagnosticsRepository(StatisticsCollectorDbContext statisticsCollectorDbContext)
    : DbEntityBaseRepository<UsersDiagnostics>(statisticsCollectorDbContext), IUsersDiagnosticsRepository
{
}