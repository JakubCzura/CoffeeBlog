using CoffeeBlog.Application.Validators.SharedValidators;
using CoffeeBlog.Domain.Commands.Users;
using FluentValidation;

namespace CoffeeBlog.Application.Validators.CommandsValidators.Users;

/// <summary>
/// Validator to validate <see cref="EditEmailCommand"/>.
/// </summary>
public class EditEmailCommandValidator : AbstractValidator<EditEmailCommand>
{
    ///<summary>
    /// Default constructor.
    /// </summary>
    public EditEmailCommandValidator()
        => RuleFor(x => x.NewEmail).SetValidator(new EmailValidator());
}