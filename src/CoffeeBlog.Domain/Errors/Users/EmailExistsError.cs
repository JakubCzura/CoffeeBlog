using CoffeeBlog.Domain.Resources;
using FluentResults;

namespace CoffeeBlog.Domain.Errors.Users;

public class EmailExistsError() : Error(ErrorMessages.EmailAlreadyExists);
