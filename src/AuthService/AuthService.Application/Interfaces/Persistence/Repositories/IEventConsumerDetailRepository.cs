using AuthService.Domain.Entities;

namespace AuthService.Application.Interfaces.Persistence.Repositories;

/// <summary>
/// Interface for repository to perform database operations related to <see cref="EventConsumerDetail"/>.
/// </summary>
public interface IEventConsumerDetailRepository : IDbEntityBaseRepository<EventConsumerDetail>
{
}