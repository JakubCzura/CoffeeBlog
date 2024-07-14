using AuthService.Application.Interfaces.Persistence.Repositories;
using AuthService.Domain.Entities;
using AuthService.Infrastructure.Persistence.DatabaseContext;

namespace AuthService.Infrastructure.Persistence.Repositories;

/// <summary>
/// Repository to perform database operations related to <see cref="EventConsumerDetail"/>.
/// </summary>
/// <param name="authServiceDbContext">Database context.</param>
internal class EventConsumerDetailRepository(AuthServiceDbContext authServiceDbContext)
    : DbEntityBaseRepository<EventConsumerDetail>(authServiceDbContext), IEventConsumerDetailRepository
{
}