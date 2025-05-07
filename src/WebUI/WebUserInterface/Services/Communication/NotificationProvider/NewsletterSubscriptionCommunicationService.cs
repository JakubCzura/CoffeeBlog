using Shared.Application.Common.Responses.Basics;
using Shared.Application.NotificationProvider.Commands.NewsletterSubscriptions.CancelNewsletterSubscription;
using Shared.Application.NotificationProvider.Commands.NewsletterSubscriptions.ConfirmNewsletterSubscription;
using Shared.Application.NotificationProvider.Commands.NewsletterSubscriptions.SubscribeNewsletter;
using System.Net.Http.Json;
using WebUserInterface.Constants.Communication;
using WebUserInterface.Services.Communication.NotificationProvider.Interfaces;

namespace WebUserInterface.Services.Communication.NotificationProvider;

public class NewsletterSubscriptionCommunicationService(IHttpClientFactory httpClientFactory) : INewsletterSubscriptionCommunicationService
{
    private readonly HttpClient _httpClient = httpClientFactory.CreateClient(HttpClientConstants.ApiGateway);

    public async Task<ResponseBase> SubscribeAsync(SubscribeNewsletterCommand subscribeNewsletterCommand,
                                                   CancellationToken cancellationToken)
    {
        HttpResponseMessage response = await _httpClient.PostAsJsonAsync("notification-provider/v1.0/newslettersubscription", subscribeNewsletterCommand, cancellationToken);
        return (await response.Content.ReadFromJsonAsync<ResponseBase>(cancellationToken))!;
    }

    public async Task<ResponseBase> ConfirmAsync(ConfirmNewsletterSubscriptionCommand confirmNewsletterSubscriptionCommand,
                                                 CancellationToken cancellationToken)
    {
        HttpResponseMessage response = await _httpClient.PutAsJsonAsync("notification-provider/v1.0/newslettersubscription", confirmNewsletterSubscriptionCommand, cancellationToken);
        return (await response.Content.ReadFromJsonAsync<ResponseBase>(cancellationToken))!;
    }

    public async Task<ResponseBase> CancelAsync(CancelNewsletterSubscriptionCommand cancelNewsletterSubscriptionCommand,
                                                CancellationToken cancellationToken)
    {
        HttpResponseMessage response = await _httpClient.DeleteAsync($"notification-provider/v1.0/newslettersubscription/{cancelNewsletterSubscriptionCommand.Id}", cancellationToken);
        return (await response.Content.ReadFromJsonAsync<ResponseBase>(cancellationToken))!;
    }
}