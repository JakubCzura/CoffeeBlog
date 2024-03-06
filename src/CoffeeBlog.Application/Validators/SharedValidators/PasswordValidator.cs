using CoffeeBlog.Domain.Constants;
using FluentValidation;

namespace CoffeeBlog.Application.Validators.SharedValidators;

/// <summary>
/// Validator to validate user's password.
/// Should be used to validate password when creating new user or updating existing one.
/// </summary>
public class PasswordValidator : AbstractValidator<string>
{
    public PasswordValidator()
        => RuleFor(password => password).NotEmpty().WithMessage("Password is required.")
                                        .Length(5, 50).WithMessage("Password must be between 5 and 50 characters long")
                                        .Must(y => y.Any(char.IsAsciiLetterUpper)).WithMessage("Password must have at least one upper letter")
                                        .Must(y => y.Any(char.IsAsciiLetterLower)).WithMessage("Password must have at least one lower letter")
                                        .Must(y => y.Any(char.IsDigit)).WithMessage("Password must have at least one digit")
                                        .Must(y => y.Any(x => PasswordConstants.SpecialCharacters.Contains(x))).WithMessage($"Password must have at least one special character: {PasswordConstants.SpecialCharacters}");
}