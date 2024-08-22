using StatisticsCollector.Domain.Entities;

namespace StatisticsCollector.Application.Interfaces.Persistence.Repositories;

/// <summary>
/// Interface for repository to perform database operations related to <see cref="EventConsumerDetail"/>.
/// </summary>
public interface IEventConsumerDetailRepository : IBaseRepository<EventConsumerDetail>
{
}