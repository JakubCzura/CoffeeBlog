using CoffeeBlog.Application.Validators.SharedValidators;
using CoffeeBlog.Domain.Commands.Users;
using FluentValidation;

namespace CoffeeBlog.Application.Validators.CommandsValidators.Users;

/// <summary>
/// Validator to validate <see cref="ChangeEmailCommand"/>.
/// </summary>
public class ChangeEmailCommandValidator : AbstractValidator<ChangeEmailCommand>
{
    ///<summary>
    /// Default constructor.
    /// </summary>
    public ChangeEmailCommandValidator()
        => RuleFor(x => x.NewEmail).SetValidator(new EmailValidator());
}