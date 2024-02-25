using FluentValidation;

namespace CoffeeBlog.Application.SharedValidators;

/// <summary>
/// Validator to validate user's username.
/// Should be used to validate username when creating new user or updating existing one.
/// </summary>
public class UsernameValidator : AbstractValidator<string>
{
    public UsernameValidator()
        => RuleFor(username => username).NotEmpty().WithMessage("Username is required.")
                                        .MaximumLength(100).WithMessage("Username can't have more than 100 characters");
}