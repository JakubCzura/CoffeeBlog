using FluentValidation;
using Shared.Application.Common.Validators;
using Shared.Domain.Common.Resources.Translations;

namespace Shared.Application.NotificationProvider.Commands.NewsletterSubscriptions.CancelNewsletterSubscription;

public class CancelNewsletterSubscriptionCommandValidator : FluentValidatorBase<CancelNewsletterSubscriptionCommand>
{
    public CancelNewsletterSubscriptionCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty()
                          .WithMessage(ValidatorMessages.ValueIsRequired);
    }
}