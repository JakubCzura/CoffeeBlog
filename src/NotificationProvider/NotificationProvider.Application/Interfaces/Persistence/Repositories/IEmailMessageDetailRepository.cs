using NotificationProvider.Domain.Entities;

namespace NotificationProvider.Application.Interfaces.Persistence.Repositories;

public interface IEmailMessageDetailRepository
{
    Task CreateAsync(EmailMessageDetail emailMessageDetail, 
                     CancellationToken cancellationToken);
}