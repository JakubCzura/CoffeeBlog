using CoffeeBlog.Domain.Resources;
using FluentValidation;

namespace CoffeeBlog.Application.Validators.SharedValidators;

/// <summary>
/// Validator to validate user's email.
/// Should be used to validate email when creating new user or updating existing one.
/// </summary>
public class EmailValidator : AbstractValidator<string>
{
    public EmailValidator()
        => RuleFor(email => email).NotEmpty().WithMessage(ValidatorMessages.EmailIsRequired)
                                  .MaximumLength(320).WithMessage(ValidatorMessages.EmailCantContainMoreThan320Characters)
                                  .EmailAddress().WithMessage(ValidatorMessages.EmailMustBeInValidFormat);
}