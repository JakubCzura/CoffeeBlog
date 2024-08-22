using StatisticsCollector.Domain.Entities;

namespace StatisticsCollector.Application.Interfaces.Persistence.Repositories;

/// <summary>
/// Interface for repository to perform database operations related to <see cref="UsersDiagnostics"/>.
/// </summary>
public interface IUsersDiagnosticsRepository : IBaseRepository<UsersDiagnostics>
{
}