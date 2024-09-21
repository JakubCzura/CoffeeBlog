using FluentValidation;
using Shared.Application.AuthService.Constants;
using Shared.Domain.AuthService.Resources;

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
                                        .Length(5, 50)
                                        .WithMessage(ValidatorMessages.PasswordMustBeBetween5And50CharactersLong)
                                        .Must(y => y.Any(char.IsAsciiLetterUpper))
                                        .WithMessage(ValidatorMessages.PasswordMustContainAtLeastOneUpperLetter)
                                        .Must(y => y.Any(char.IsAsciiLetterLower))
                                        .WithMessage(ValidatorMessages.PasswordMustContainAtLeastOneLowerLetter)
                                        .Must(y => y.Any(char.IsDigit))
                                        .WithMessage(ValidatorMessages.PasswordMustContainAtLeastOneDigit)
                                        .Must(y => y.Any(x => PasswordConstants.SpecialCharacters.Contains(x)))
                                        .WithMessage($"{ValidatorMessages.PasswordMustContainAtLeastOneOfSpecialCharacters}: {PasswordConstants.SpecialCharacters}");
}