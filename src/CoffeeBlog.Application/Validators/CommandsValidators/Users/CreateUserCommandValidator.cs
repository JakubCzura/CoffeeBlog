﻿using CoffeeBlog.Application.Validators.SharedValidators;
using CoffeeBlog.Domain.Commands.Users;
using CoffeeBlog.Domain.Resources;
using FluentValidation;

namespace CoffeeBlog.Application.Validators.CommandsValidators.Users;

public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
{
    public CreateUserCommandValidator()
    {
        RuleFor(x => x.Username).SetValidator(new UsernameValidator());

        //NotEqual(x => x.Username) - Important, because e-mails and usernames are unique in database.
        //Moreover there must not be any username equal to any e-mail due to this program architecture and expectations.
        RuleFor(x => x.Email).SetValidator(new EmailValidator())
                             .NotEqual(x => x.Username).WithMessage(ValidatorMessages.EmailMustBeDifferentFromUsername);

        RuleFor(x => x.Password).SetValidator(new PasswordValidator());

        RuleFor(x => x.ConfirmPassword).NotEmpty().WithMessage(ValidatorMessages.ConfirmPasswordIsRequired)
                                       .Equal(x => x.Password).WithMessage(ValidatorMessages.PasswordAndConfirmPasswordMustMatch);
    }
}