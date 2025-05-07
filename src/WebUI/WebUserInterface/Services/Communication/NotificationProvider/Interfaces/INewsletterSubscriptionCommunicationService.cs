using Shared.Application.Common.Responses.Basics;
using Shared.Application.NotificationProvider.Commands.NewsletterSubscriptions.CancelNewsletterSubscription;
using Shared.Application.NotificationProvider.Commands.NewsletterSubscriptions.ConfirmNewsletterSubscription;
using Shared.Application.NotificationProvider.Commands.NewsletterSubscriptions.SubscribeNewsletter;

namespace WebUserInterface.Services.Communication.NotificationProvider.Interfaces;

public interface INewsletterSubscriptionCommunicationService
{
    Task<ResponseBase> SubscribeAsync(SubscribeNewsletterCommand subscribeNewsletterCommand,
                                      CancellationToken cancellationToken);

    Task<ResponseBase> ConfirmAsync(ConfirmNewsletterSubscriptionCommand confirmNewsletterSubscriptionCommand,
                                    CancellationToken cancellationToken);

    Task<ResponseBase> CancelAsync(CancelNewsletterSubscriptionCommand cancelNewsletterSubscriptionCommand,
                                   CancellationToken cancellationToken);
}