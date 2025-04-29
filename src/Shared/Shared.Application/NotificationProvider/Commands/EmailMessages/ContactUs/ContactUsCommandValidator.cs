using FluentValidation;
using Shared.Application.Common.Validators;

namespace Shared.Application.NotificationProvider.Commands.EmailMessages.ContactUs;

public class ContactUsCommandValidator : FluentValidatorBase<ContactUsCommand>
{
    public ContactUsCommandValidator()
    {
        RuleFor(x => x.Name).NotEmpty()
                            .WithMessage("Name is required.")
                            .MaximumLength(50)
                            .WithMessage("Name cannot exceed 50 characters.");

        RuleFor(x => x.Surname).NotEmpty()
                               .WithMessage("Surname is required.")
                               .MaximumLength(50)
                               .WithMessage("Surname cannot exceed 50 characters.");

        RuleFor(x => x.Email).NotEmpty()
                             .WithMessage("Email is required.")
                             .EmailAddress()
                             .WithMessage("Invalid email address.");

        RuleFor(x => x.Message).NotEmpty()
                               .WithMessage("Message is required.")
                               .MaximumLength(1000)
                               .WithMessage("Message cannot exceed 1000 characters.");
    }
}