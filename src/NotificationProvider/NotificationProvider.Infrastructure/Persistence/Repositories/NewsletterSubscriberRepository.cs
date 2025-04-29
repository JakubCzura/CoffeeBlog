using Microsoft.EntityFrameworkCore;
using NotificationProvider.Application.Interfaces.Persistence.Repositories;
using NotificationProvider.Domain.Entities;
using NotificationProvider.Infrastructure.Persistence.DatabaseContext;

namespace NotificationProvider.Infrastructure.Persistence.Repositories;

/// <summary>
/// Repository to perform database operations related to <see cref="NewsletterSubscriber"/>.
/// </summary>
internal class NewsletterSubscriberRepository(NotificationProviderDbContext notificationProviderDbContext) 
    : BaseRepository<NewsletterSubscriber>(notificationProviderDbContext), INewsletterSubscriberRepository
{
    public async Task<bool> EmailExistsAsync(string email, CancellationToken cancellationToken)
        => await notificationProviderDbContext.NewsletterSubscribers.AsNoTracking()
                                                                     .AnyAsync(s => s.Email == email, cancellationToken);
}
