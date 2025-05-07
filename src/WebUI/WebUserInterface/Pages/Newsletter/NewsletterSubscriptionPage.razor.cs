using MudBlazor;
using Shared.Application.Common.Responses.Basics;
using Shared.Application.NotificationProvider.Commands.NewsletterSubscriptions.SubscribeNewsletter;
using Shared.Domain.Common.Resources.Translations;
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
            resultMessage.ResponseMessage = ResponseMessages.ThankYouForSubscribing_PleaseCheckYourEmailToConfirmYourSubscription;
        }
        else
        {
            resultMessage.ResponseMessage ??= ResponseMessages.AnErrorOccurredWhileProcessingYourRequest_PleaseTryAgain;
        }

        isProcessing = false;
    }
}