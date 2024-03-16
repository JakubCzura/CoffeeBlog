using CoffeeBlog.Application.Validators.SharedValidators;
using CoffeeBlog.Domain.Commands.Users;
using FluentValidation;

namespace CoffeeBlog.Application.Validators.CommandsValidators.Users;

/// <summary>
/// Validator to validate <see cref="ChangeUsernameCommand"/>.
/// </summary>
public class ChangeUsernameCommandValidator : AbstractValidator<ChangeUsernameCommand>
{
    ///<summary>
    /// Default constructor.
    /// </summary>
    public ChangeUsernameCommandValidator() 
        => RuleFor(x => x.NewUsername).SetValidator(new UsernameValidator());
}