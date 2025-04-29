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
    public Task<bool> EmailExistsAsync(string email,
                                       CancellationToken cancellationToken);
}