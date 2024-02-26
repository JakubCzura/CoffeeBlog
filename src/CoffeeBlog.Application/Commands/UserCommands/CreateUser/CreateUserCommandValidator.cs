using CoffeeBlog.Application.SharedValidators;
using FluentValidation;

namespace CoffeeBlog.Application.Commands.UserCommands.CreateUser;

public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
{
    public CreateUserCommandValidator()
    {
        RuleFor(x => x.Username).SetValidator(new UsernameValidator());

        //NotEqual(x => x.Username) - Important, because e-mails and usernames are unique in database.
        //Moreover there must not be any username equal to any e-mail due to this program architecture and expectations.
        RuleFor(x => x.Email).SetValidator(new EmailValidator())
                             .NotEqual(x => x.Username).WithMessage("E-mail must be different from username");

        RuleFor(x => x.Password).SetValidator(new PasswordValidator());

        RuleFor(x => x.ConfirmPassword).NotEmpty().WithMessage("Confirm password is required.")
                                       .Equal(x => x.Password).WithMessage("Password and confirm password must match.");
    }
}