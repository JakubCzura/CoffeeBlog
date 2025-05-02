using FluentValidation;
using Shared.Application.Common.Validators;
using Shared.Domain.Common.Resources.Translations;

namespace Shared.Application.NotificationProvider.Commands.NewsletterSubscriptions.ConfirmNewsletterSubscription;

public class ConfirmNewsletterSubscriptionCommandValidator : FluentValidatorBase<ConfirmNewsletterSubscriptionCommand>
{
    public ConfirmNewsletterSubscriptionCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty()
                          .WithMessage(ValidatorMessages.ValueIsRequired);
    }
}