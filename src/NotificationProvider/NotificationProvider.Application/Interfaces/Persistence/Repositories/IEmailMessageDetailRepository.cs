using NotificationProvider.Domain.Entities;

namespace NotificationProvider.Application.Interfaces.Persistence.Repositories;

/// <summary>
/// Interface for repository to perform database operations related to <see cref="EmailMessageDetail"/>.
/// </summary>
public interface IEmailMessageDetailRepository : IDbEntityBaseRepository<EmailMessageDetail>
{
}