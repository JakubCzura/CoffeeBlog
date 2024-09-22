using FluentValidation;
using Shared.Application.AuthService.Constants.Policy;
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
                                  .MaximumLength(EmailPolicyConstants.MaxLength)
                                  .WithMessage(string.Format(ValidatorMessages.EmailCantContainMoreThan_0_Characters, EmailPolicyConstants.MaxLength))
                                  .EmailAddress()
                                  .WithMessage(ValidatorMessages.EmailMustBeInValidFormat);
}