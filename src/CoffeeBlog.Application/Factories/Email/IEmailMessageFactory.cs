﻿using CoffeeBlog.Domain.Models.Email;

namespace CoffeeBlog.Application.Factories.Email;

public interface IEmailMessageFactory
{
    IEmailMessage CreateWelcomeEmailMessage(string to,
                                            string nickname);

    IEmailMessage CreatePasswordResetEmailMessage(string to,
                                                  string nickname,
                                                  string token,
                                                  DateTime expirationDate);
}