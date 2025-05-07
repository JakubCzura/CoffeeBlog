using FluentValidation.Results;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using Shared.Application.Common.Responses.Basics;
using Shared.Application.NotificationProvider.Commands.NewsletterSubscriptions.CancelNewsletterSubscription;
using Shared.Domain.Common.Resources.Translations;
using WebUserInterface.Services.Communication.NotificationProvider.Interfaces;

namespace WebUserInterface.Pages.Newsletter;

public partial class NewsletterCancellingPage(INewsletterSubscriptionCommunicationService newsletterSubscriptionCommunicationService, 
                                              ISnackbar snackbar)
{
    [Parameter]
    public string Id { get; set; } = string.Empty;

    private bool isProcessing = false;
    private bool isSuccess = false;
    private CancelNewsletterSubscriptionCommand cancelNewsletterSubscriptionCommand = new();
    private CancelNewsletterSubscriptionCommandValidator cancelNewsletterSubscriptionCommandValidator = new();

    protected override async Task OnInitializedAsync()
    {
        cancelNewsletterSubscriptionCommand.Id = Id;
        await Submit();
    }

    private async Task Submit()
    {
        if (isProcessing)
        {
            return;
        }

        ValidationResult validationResult = cancelNewsletterSubscriptionCommandValidator.Validate(cancelNewsletterSubscriptionCommand);
        if (!validationResult.IsValid)
        {
            snackbar.Add(ResponseMessages.AnErrorOccurredWhileProcessingYourRequest_PleaseTryAgain, Severity.Error);
            return;
        }

        isProcessing = true;

        ResponseBase response = await newsletterSubscriptionCommunicationService.CancelAsync(cancelNewsletterSubscriptionCommand, default);
        if (response.IsSuccess)
        {
            isSuccess = true;
        }
        else
        {
            snackbar.Add(response.ResponseMessage ?? ResponseMessages.AnErrorOccurredWhileProcessingYourRequest_PleaseTryAgain, Severity.Error);
        }

        isProcessing = false;
    }
}