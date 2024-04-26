using NotificationProvider.Application.Interfaces.Persistence.Repositories;
using NotificationProvider.Domain.Entities;

namespace NotificationProvider.Infrastructure.Persistence.Repositories;

internal class EmailMessageDetailRepository : IEmailMessageDetailRepository
{
    public Task CreateAsync(EmailMessageDetail emailMessageDetail, 
                            CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}