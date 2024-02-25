using CoffeeBlog.Application.SharedValidators;
using FluentValidation;

namespace CoffeeBlog.Application.Commands.User.CreateUser;

public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
{
    public CreateUserCommandValidator()
    {
        RuleFor(x => x.Username).NotEmpty().WithMessage("Username is required.")
                                .Length(3, 100).WithMessage("Username must be between 3 and 100 characters long");

        //NotEqual(x => x.Username) - Important, because e-mails and usernames are unique in database.
        //Moreover there must not be any username equal to any e-mail due to this program architecture and expectations.
        RuleFor(x => x.Email).NotEmpty().WithMessage("Email is required.")
                             .MaximumLength(320).WithMessage("E-mail can't have more than 320 characters")
                             .NotEqual(x => x.Username).WithMessage("E-mail must be different from username")
                             .EmailAddress().WithMessage("E-mail must be in valid format");

        RuleFor(x => x.Password).SetValidator(new PasswordValidator());

        RuleFor(x => x.ConfirmPassword).NotEmpty().WithMessage("Confirm password is required.")
                                       .Equal(x => x.Password).WithMessage("Password and confirm password must match.");
    }
}