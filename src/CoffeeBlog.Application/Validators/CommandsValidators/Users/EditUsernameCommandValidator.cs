using CoffeeBlog.Application.Validators.SharedValidators;
using CoffeeBlog.Domain.Commands.Users;
using FluentValidation;

namespace CoffeeBlog.Application.Validators.CommandsValidators.Users;

/// <summary>
/// Validator to validate <see cref="EditUsernameCommand"/>.
/// </summary>
public class EditUsernameCommandValidator : AbstractValidator<EditUsernameCommand>
{
    ///<summary>
    /// Default constructor.
    /// </summary>
    public EditUsernameCommandValidator()
        => RuleFor(x => x.NewUsername).SetValidator(new UsernameValidator());
}