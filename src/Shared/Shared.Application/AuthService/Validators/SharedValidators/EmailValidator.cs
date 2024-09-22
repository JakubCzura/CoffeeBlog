using FluentValidation;
using Shared.Domain.Common.Resources.Translations;

namespace Shared.Application.AuthService.Validators.SharedValidators;

/// <summary>
/// Validator to validate user's email.
/// </summary>
public class EmailValidator : AbstractValidator<string>
{
    /// <summary>
    /// Default constructor.
    /// </summary>
    public EmailValidator()
        => RuleFor(email => email).NotEmpty()
                                  .WithMessage(ValidatorMessages.EmailIsRequired)
                                  .MaximumLength(320)
                                  .WithMessage(ValidatorMessages.EmailCantContainMoreThan320Characters)
                                  .EmailAddress()
                                  .WithMessage(ValidatorMessages.EmailMustBeInValidFormat);
}