using Microsoft.EntityFrameworkCore;
using NotificationProvider.Application.Interfaces.Persistence.Repositories;
using NotificationProvider.Domain.Entities;
using NotificationProvider.Domain.Enums;
using NotificationProvider.Infrastructure.Persistence.DatabaseContext;

namespace NotificationProvider.Infrastructure.Persistence.Repositories;

/// <summary>
/// Repository to perform database operations related to <see cref="EmailMessage"/>.
/// </summary>
/// <param name="_notificationProviderDbContext">Database context.</param>
internal class EmailMessageRepository(NotificationProviderDbContext _notificationProviderDbContext)
    : BaseRepository<EmailMessage>(_notificationProviderDbContext), IEmailMessageRepository
{
    public Task<List<EmailMessage>> GetMessagesToSendAsync(CancellationToken cancellationToken)
        => _notificationProviderDbContext.EmailMessages
            .Where(p => p.MessageStatus != EmailMessageStatus.Sent &&
                        p.SentAt < DateTime.UtcNow &&
                        p.SentAt > DateTime.UtcNow.AddDays(-3) &&
                        p.SendErrorCount < 5)
            .OrderBy(p => p.SentAt)
            .Take(50)
            .ToListAsync(cancellationToken);
}