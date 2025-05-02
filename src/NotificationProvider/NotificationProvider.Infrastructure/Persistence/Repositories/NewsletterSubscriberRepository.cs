using Microsoft.EntityFrameworkCore;
using MongoDB.Bson;
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
    public async Task<bool> EmailExistsAsync(string email, 
                                             CancellationToken cancellationToken)
        => await notificationProviderDbContext.NewsletterSubscribers.AsNoTracking()
                                                                    .AnyAsync(s => s.Email == email, cancellationToken);

    public async Task<int> ConfirmSubscriptionAsync(ObjectId id, 
                                                    CancellationToken cancellationToken)
        => await notificationProviderDbContext.NewsletterSubscribers.Where(s => s.Id == id)
                                                                    .ExecuteUpdateAsync(s => s.SetProperty(x => x.IsConfirmed, true), cancellationToken);
}