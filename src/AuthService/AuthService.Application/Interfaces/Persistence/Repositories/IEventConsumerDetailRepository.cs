using AuthService.Domain.Entities;

namespace AuthService.Application.Interfaces.Persistence.Repositories;

public interface IEventConsumerDetailRepository : IDbEntityBaseRepository<EventConsumerDetail>
{
}