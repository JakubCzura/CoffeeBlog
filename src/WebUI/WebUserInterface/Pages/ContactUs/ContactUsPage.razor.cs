using MudBlazor;
using Shared.Application.NotificationProvider.Commands.EmailMessages.ContactUs;

namespace WebUserInterface.Pages.ContactUs;

public partial class ContactUsPage
{
    private string resultMessage = string.Empty;
    private bool isProcessing = false;

    private MudForm contactUsForm;
    private ContactUsCommand contactUsCommand = new();
    private ContactUsCommandValidator contactUsCommandValidator = new();

    private async Task Submit()
    {
        if (isProcessing)
        {
            return;
        }

        await contactUsForm.Validate();
        if (!contactUsForm.IsValid)
        {
            return;
        }

        isProcessing = true;

        // TODO: Implement logic when working on backend

        resultMessage = "Thank you for reaching out! We'll get back to you soon.";

        isProcessing = false;
    }
}