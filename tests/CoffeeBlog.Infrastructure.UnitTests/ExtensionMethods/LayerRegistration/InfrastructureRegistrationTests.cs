using AuthService.Application.Email;
using AuthService.Application.Factories.Emails;
using AuthService.Application.Interfaces.Helpers;
using AuthService.Application.Interfaces.Persistence.Repositories;
using AuthService.Application.Interfaces.Security.Authentication;
using AuthService.Application.Interfaces.Security.Password;
using AuthService.Infrastructure.ExtensionMethods.LayerRegistration;
using AuthService.Infrastructure.UnitTests.HelpersForTests;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AuthService.Infrastructure.UnitTests.ExtensionMethods.LayerRegistration;

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