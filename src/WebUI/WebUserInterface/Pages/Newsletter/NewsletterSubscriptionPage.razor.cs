using MudBlazor;
using Shared.Application.Common.Responses.Basics;
using Shared.Application.NotificationProvider.Commands.NewsletterSubscriptions.SubscribeNewsletter;
using WebUserInterface.Services.Communication.NotificationProvider.Interfaces;

namespace WebUserInterface.Pages.Newsletter;

public partial class NewsletterSubscriptionPage(INewsletterSubscriptionCommunicationService newsletterSubscriptionCommunicationService)
{
    private ResponseBase resultMessage = new();
    private bool isProcessing = false;

    private MudForm subscribeNewsletterForm;
    private SubscribeNewsletterCommand subscribeNewsletterCommand = new();
    private SubscribeNewsletterCommandValidator subscribeNewsletterCommandValidator = new();

    private async Task Submit()
    {
        if (isProcessing)
        {
            return;
        }

        await subscribeNewsletterForm.Validate();
        if (!subscribeNewsletterForm.IsValid)
        {
            return;
        }

        isProcessing = true;

        ResponseBase response = await newsletterSubscriptionCommunicationService.SubscribeAsync(subscribeNewsletterCommand, default);
        if (response.IsSuccess)
        {
            resultMessage.ResponseMessage = "Thank you for subscribing! Please check your e-mail to confirm your subscription.";
        }
        else
        {
            resultMessage.ResponseMessage ??= "An error occurred while processing your request. Please try again later.";
        }

        isProcessing = false;
    }
}