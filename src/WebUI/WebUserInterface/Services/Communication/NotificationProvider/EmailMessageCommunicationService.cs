using Shared.Application.Common.Responses.Basics;
using Shared.Application.NotificationProvider.Commands.EmailMessages.ContactUs;
using System.Net.Http.Json;
using WebUserInterface.Constants.Communication;
using WebUserInterface.Services.Communication.NotificationProvider.Interfaces;

namespace WebUserInterface.Services.Communication.NotificationProvider;

public class EmailMessageCommunicationService(IHttpClientFactory httpClientFactory) : IEmailMessageCommunicationService
{
    private readonly HttpClient _httpClient = httpClientFactory.CreateClient(HttpClientConstants.ApiGateway);

    public async Task<ResponseBase> ContactUsAsync(ContactUsCommand contactUsCommand, 
                                                   CancellationToken cancellationToken)
    {
        HttpResponseMessage response = await _httpClient.PostAsJsonAsync("notification-provider/v1.0/emailmessage/contact", contactUsCommand, cancellationToken);
        return (await response.Content.ReadFromJsonAsync<ResponseBase>(cancellationToken))!;
    }
}