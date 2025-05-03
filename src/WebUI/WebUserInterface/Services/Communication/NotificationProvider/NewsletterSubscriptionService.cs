using Shared.Application.Common.Responses.Basics;
using Shared.Application.NotificationProvider.Commands.NewsletterSubscriptions.CancelNewsletterSubscription;
using Shared.Application.NotificationProvider.Commands.NewsletterSubscriptions.ConfirmNewsletterSubscription;
using Shared.Application.NotificationProvider.Commands.NewsletterSubscriptions.SubscribeNewsletter;
using System.Net.Http.Json;
using WebUserInterface.Constants.Communication;

namespace WebUserInterface.Services.Communication.NotificationProvider;

public class NewsletterSubscriptionService(IHttpClientFactory httpClientFactory)
{
    private readonly HttpClient _httpClient = httpClientFactory.CreateClient(HttpClientConstants.ApiGateway);

    public async Task<ResponseBase> SubscribeAsync(SubscribeNewsletterCommand subscribeNewsletterCommand,
                                                   CancellationToken cancellationToken)
    {
        HttpResponseMessage response = await _httpClient.PostAsJsonAsync("notification-provider/v1.0/newslettersubscription/subscribe", subscribeNewsletterCommand, cancellationToken);
        return (await response.Content.ReadFromJsonAsync<ResponseBase>(cancellationToken))!;
    }

    public async Task<ResponseBase> ConfirmAsync(ConfirmNewsletterSubscriptionCommand confirmNewsletterSubscriptionCommand,
                                                 CancellationToken cancellationToken)
    {
        HttpResponseMessage response = await _httpClient.PostAsJsonAsync("notification-provider/v1.0/newslettersubscription/confirm", confirmNewsletterSubscriptionCommand, cancellationToken);
        return (await response.Content.ReadFromJsonAsync<ResponseBase>(cancellationToken))!;
    }

    public async Task<ResponseBase> CancelAsync(CancelNewsletterSubscriptionCommand cancelNewsletterSubscriptionCommand,
                                                CancellationToken cancellationToken)
    {
        HttpResponseMessage response = await _httpClient.PostAsJsonAsync("notification-provider/v1.0/newslettersubscription/cancel", cancelNewsletterSubscriptionCommand, cancellationToken);
        return (await response.Content.ReadFromJsonAsync<ResponseBase>(cancellationToken))!;
    }
}