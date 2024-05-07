using StatisticsCollector.Application.Interfaces.Persistence.Repositories;
using StatisticsCollector.Domain.Entities;
using StatisticsCollector.Infrastructure.Persistence.DatabaseContext;

namespace StatisticsCollector.Infrastructure.Persistence.Repositories;

internal class UserDiagnosticRepository(StatisticsCollectorDbContext statisticsCollectorDbContext)
    : DbEntityBaseRepository<UserDiagnostic>(statisticsCollectorDbContext), IUserDiagnosticRepository
{
}