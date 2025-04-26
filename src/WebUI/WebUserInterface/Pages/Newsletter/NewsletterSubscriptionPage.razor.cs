namespace WebUserInterface.Pages.Newsletter;

public partial class NewsletterSubscriptionPage
{
    //TODO: Use command instead of "email" variable when working on backend
    private string email = string.Empty;

    private bool agreeToTerms = false;
    private bool isValid = false;
    private string resultMessage = string.Empty;
    private bool isProcessing = false;

    private void Submit()
    {
        if (isProcessing)
        {
            return;
        }

        if (!isValid)
        {
            return;
        }

        isProcessing = true;

        // TODO: Implement logic when working on backend

        resultMessage = "Thank you for subscribing! Please check your e-mail to confirm your subscription.";

        isProcessing = false;
    }
}