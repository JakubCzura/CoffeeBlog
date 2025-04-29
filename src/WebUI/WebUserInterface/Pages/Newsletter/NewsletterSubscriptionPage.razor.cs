using MudBlazor;
using Shared.Application.NotificationProvider.Commands.NewsletterSubscriptions.SubscribeNewsletter;

namespace WebUserInterface.Pages.Newsletter;

public partial class NewsletterSubscriptionPage
{
    private string resultMessage = string.Empty;
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

        // TODO: Implement logic when working on backend

        resultMessage = "Thank you for subscribing! Please check your e-mail to confirm your subscription.";

        isProcessing = false;
    }
}