using FluentValidation;
using Shared.Application.Common.Validators;
using Shared.Domain.Common.Resources.Translations;

namespace Shared.Application.NotificationProvider.Commands.NewsletterSubscriptions.SubscribeNewsletter;

public class SubscribeNewsletterCommandValidator : FluentValidatorBase<SubscribeNewsletterCommand>
{
    public SubscribeNewsletterCommandValidator()
    {
        RuleFor(x => x.Email).NotEmpty()
                             .WithMessage(ValidatorMessages.EmailIsRequired)
                             .EmailAddress()
                             .WithMessage(ValidatorMessages.EmailMustBeInValidFormat);

        RuleFor(x => x.AgreeToTerms).NotEmpty()
                                    .WithMessage(ValidatorMessages.YouMustAgreeToTheTermsAndConditions);
    }
}