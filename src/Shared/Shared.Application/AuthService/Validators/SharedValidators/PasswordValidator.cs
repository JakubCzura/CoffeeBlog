using FluentValidation;
using Shared.Application.AuthService.Constants.Policy;
using Shared.Domain.Common.Resources.Translations;

namespace Shared.Application.AuthService.Validators.SharedValidators;

/// <summary>
/// Validator to validate user's password.
/// </summary>
public class PasswordValidator : AbstractValidator<string>
{
    /// <summary>
    /// Default constructor.
    /// </summary>
    public PasswordValidator()
        => RuleFor(password => password).NotEmpty()
                                        .WithMessage(ValidatorMessages.PasswordIsRequired)
                                        .Length(PasswordPolicyConstants.MinLength, PasswordPolicyConstants.MaxLength)
                                        .WithMessage(string.Format(ValidatorMessages.PasswordMustBeBetween_0_And_1_CharactersLong, PasswordPolicyConstants.MinLength, PasswordPolicyConstants.MaxLength))
                                        .Must(y => y.Any(char.IsAsciiLetterUpper))
                                        .WithMessage(ValidatorMessages.PasswordMustContainAtLeastOneUpperLetter)
                                        .Must(y => y.Any(char.IsAsciiLetterLower))
                                        .WithMessage(ValidatorMessages.PasswordMustContainAtLeastOneLowerLetter)
                                        .Must(y => y.Any(char.IsDigit))
                                        .WithMessage(ValidatorMessages.PasswordMustContainAtLeastOneDigit)
                                        .Must(y => y.Any(x => PasswordPolicyConstants.SpecialCharacters.Contains(x)))
                                        .WithMessage(string.Format(ValidatorMessages.PasswordMustContainAtLeastOneOfSpecialCharacters__0_, PasswordPolicyConstants.SpecialCharacters));
}