using FluentValidation;

namespace CoffeeBlog.Application.SharedValidators;

public class PasswordValidator : AbstractValidator<string>
{
    public PasswordValidator()
        => RuleFor(password => password).NotEmpty().WithMessage("Password is required.")
                                        .Length(5, 50).WithMessage("Password must be between 5 and 50 characters long")
                                        .Must(y => y.Any(char.IsAsciiLetterUpper)).WithMessage("Password must have at least one upper letter")
                                        .Must(y => y.Any(char.IsAsciiLetterLower)).WithMessage("Password must have at least one lower letter")
                                        .Must(y => y.Any(char.IsDigit)).WithMessage("Password must have at least one digit");
}