using MudBlazor;
using Shared.Application.Common.Responses.Basics;
using Shared.Application.NotificationProvider.Commands.EmailMessages.ContactUs;
using Shared.Application.NotificationProvider.Commands.NewsletterSubscriptions.SubscribeNewsletter;
using Shared.Domain.Common.Resources.Translations;
using WebUserInterface.Services.Communication.NotificationProvider;
using WebUserInterface.Services.Communication.NotificationProvider.Interfaces;

namespace WebUserInterface.Pages.ContactUs;

public partial class ContactUsPage(IEmailMessageCommunicationService emailMessageCommunicationService)
{
    private ResponseBase resultMessage = new();
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

        ResponseBase response = await emailMessageCommunicationService.ContactUsAsync(contactUsCommand, default);
        if (response.IsSuccess)
        {
            resultMessage.ResponseMessage = ResponseMessages.ThankYouForReachingOut_WeWillGetBackToYouSoon;
        }
        else
        {
            resultMessage.ResponseMessage ??= ResponseMessages.AnErrorOccurredWhileProcessingYourRequest_PleaseTryAgain;
        }

        isProcessing = false;
    }
}