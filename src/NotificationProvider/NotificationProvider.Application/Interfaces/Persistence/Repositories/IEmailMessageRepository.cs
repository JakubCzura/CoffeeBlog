using NotificationProvider.Domain.Entities;

namespace NotificationProvider.Application.Interfaces.Persistence.Repositories;

/// <summary>
/// Interface for repository to perform database operations related to <see cref="EmailMessage"/>.
/// </summary>
public interface IEmailMessageRepository : IBaseRepository<EmailMessage>
{
    Task<List<EmailMessage>> GetMessagesToSendAsync(CancellationToken cancellationToken);

}