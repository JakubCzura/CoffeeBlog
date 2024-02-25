using FluentValidation;

namespace CoffeeBlog.Application.SharedValidators;

/// <summary>
/// Validator to validate user's email.
/// Should be used to validate email when creating new user or updating existing one.
/// </summary>
public class EmailValidator : AbstractValidator<string>
{
    public EmailValidator()
        => RuleFor(email => email).NotEmpty().WithMessage("Email is required.")
                                  .MaximumLength(320).WithMessage("E-mail can't have more than 320 characters")
                                  .EmailAddress().WithMessage("E-mail must be in valid format");
}