using MongoDB.Bson;
using NotificationProvider.Domain.Entities;

namespace NotificationProvider.Application.Interfaces.Persistence.Repositories;

/// <summary>
/// Interface for repository to perform database operations related to <see cref="NewsletterSubscriber"/>.
/// </summary>
public interface INewsletterSubscriberRepository : IBaseRepository<NewsletterSubscriber>
{
    /// <summary>
    /// Checks if a newsletter subscriber with the given email exists.
    /// </summary>
    /// <param name="email">The email to check.</param>
    /// <param name="cancellationToken">Token to cancel asynchronous operation.</param>
    /// <returns>True if the email exists, otherwise false.</returns>
    Task<bool> EmailExistsAsync(string email,
                                CancellationToken cancellationToken);

    /// <summary>
    /// Confirms the subscription of a newsletter subscriber by setting the IsConfirmed property to true.
    /// </summary>
    /// <param name="id">Id of subscription to confirm.</param>
    /// <param name="cancellationToken">Token to cancel asynchronous operation.</param>
    /// <returns>Number of rows edited in database.</returns>
    Task<int> ConfirmSubscriptionAsync(ObjectId id,
                                       CancellationToken cancellationToken);
}