using CoffeeBlog.Domain.Models.Email;

namespace CoffeeBlog.Application.Email;

public interface IEmailServiceProvider
{
    Task<string> SendEmailAsync(IEmailMessage emailMessage);
}