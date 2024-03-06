using CoffeeBlog.Application.Email;
using CoffeeBlog.Application.Factories.Emails;
using CoffeeBlog.Application.Interfaces.Helpers;
using CoffeeBlog.Application.Interfaces.Persistence.Repositories;
using CoffeeBlog.Application.Interfaces.Security.Authentication;
using CoffeeBlog.Application.Interfaces.Security.Password;
using CoffeeBlog.Infrastructure.ExtensionMethods;
using CoffeeBlog.Infrastructure.UnitTests.HelpersForTests;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CoffeeBlog.Infrastructure.UnitTests.ExtensionMethods;

public class InfrastructureRegistrationTests
{
    private readonly IConfiguration _configuration = ConfigurationProviderHelper.InitializeConfiguration();

    [Fact]
    public void DI_AddInfrastructureDI_should_ImplementServices_when_Called()
    {
        // Arrange
        ServiceCollection services = new();

        // Act
        services.AddInfrastructureDI(_configuration);
        IServiceProvider serviceProvider = services.BuildServiceProvider();

        // Assert
        services.Should().NotBeEmpty();
        serviceProvider.GetService<IApiErrorRepository>().Should().NotBeNull();
        serviceProvider.GetService<IRequestDetailRepository>().Should().NotBeNull();
        serviceProvider.GetService<IRoleRepository>().Should().NotBeNull();
        serviceProvider.GetService<IUserDetailRepository>().Should().NotBeNull();
        serviceProvider.GetService<IUserRepository>().Should().NotBeNull();
        serviceProvider.GetService<IDateTimeProvider>().Should().NotBeNull();
        serviceProvider.GetService<IJwtService>().Should().NotBeNull();
        serviceProvider.GetService<IPasswordHasher>().Should().NotBeNull();
        serviceProvider.GetService<IEmailMessageFactory>().Should().NotBeNull();
        serviceProvider.GetService<IEmailServiceProvider>().Should().NotBeNull();
    }
}