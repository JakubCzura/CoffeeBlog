using FluentValidation.Results;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using Shared.Application.NotificationProvider.Commands.NewsletterSubscriptions.ConfirmNewsletterSubscription;
using Shared.Domain.Common.Resources.Translations;

namespace WebUserInterface.Pages.Newsletter;

public partial class NewsletterConfirmationPage(ISnackbar snackbar)
{
    [Parameter]
    public string Id { get; set; } = string.Empty;

    private bool isProcessing = false;
    private bool isSuccess = false;

    private ConfirmNewsletterSubscriptionCommand confirmNewsletterSubscriptionCommand = new();
    private ConfirmNewsletterSubscriptionCommandValidator confirmNewsletterSubscriptionCommandValidator = new();

    protected override async Task OnInitializedAsync()
    {
        confirmNewsletterSubscriptionCommand.Id = Id;
        await Submit();
    }

    private async Task Submit()
    {
        if (isProcessing)
        {
            return;
        }

        ValidationResult validationResult = confirmNewsletterSubscriptionCommandValidator.Validate(confirmNewsletterSubscriptionCommand);
        if (!validationResult.IsValid)
        {
            snackbar.Add(ResponseMessages.AnErrorOccurredWhileProcessingYourRequest_PleaseTryAgain, Severity.Error);
            return;
        }

        isProcessing = true;

        // TODO: Replace with actual API call to confirm the user's subscription instead of simulating delay
        await Task.Delay(2000);
        isSuccess = true;

        isProcessing = false;
    }
}