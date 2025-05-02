using FluentValidation.Results;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using Shared.Application.NotificationProvider.Commands.NewsletterSubscriptions.CancelNewsletterSubscription;
using Shared.Domain.Common.Resources.Translations;

namespace WebUserInterface.Pages.Newsletter;

public partial class NewsletterCancellingPage(ISnackbar snackbar)
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

        // TODO: Replace with actual API call to unsubscribe the user instead of simulating delay
        await Task.Delay(2000);
        isSuccess = true;

        isProcessing = false;
    }
}