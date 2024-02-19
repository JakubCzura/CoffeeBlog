using CoffeeBlog.Domain.SettingsOptions.PasswordHasher;
using CoffeeBlog.Infrastructure.Security.Password;
using CoffeeBlog.Infrastructure.UnitTests.HelpersForTests;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace CoffeeBlog.Infrastructure.UnitTests.Security.Password;

public class PasswordHasherTests
{
    private readonly IOptions<PasswordHasherOptions> _passwordHasherOptions = Options.Create(ConfigurationProviderHelper.InitializeConfiguration()
                                                                                                                        .GetSection(PasswordHasherOptions.AppsettingsKey)
                                                                                                                        .Get<PasswordHasherOptions>()!);

    private readonly PasswordHasher _passwordHasher;

    public PasswordHasherTests() => _passwordHasher = new PasswordHasher(_passwordHasherOptions);

    [Theory]
    [InlineData("1")]
    [InlineData("B")]
    [InlineData("Password123!")]
    [InlineData("##Pass!@word#123098#@43")]
    public void HashPassword_should_ReturnHashedPassword_when_GivenPasswordIsValid(string password)
    {
        // Act
        string result = _passwordHasher.HashPassword(password);

        // Assert
        result.Should().NotBeNullOrEmpty();
        result.Should().NotBe(password);
        result.Split(_passwordHasherOptions.Value.Delimiter).Should().HaveCount(2);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    public void HashPassword_should_ThrowArgumentNullException_when_GivenPasswordIsEmpty(string password)
        => _passwordHasher.Invoking(x => x.HashPassword(password))
                          .Should()
                          .Throw<ArgumentNullException>();

    [Theory]
    [InlineData("1")]
    [InlineData("B")]
    [InlineData("Password123!")]
    [InlineData("##Pass!@word#123098#@43")]
    public void VerifyPassword_should_ReturnTrue_when_GivenPasswordIsValid(string password)
    {
        // Act
        string hashedPassword = _passwordHasher.HashPassword(password);
        bool result = _passwordHasher.VerifyPassword(password, hashedPassword);

        // Assert
        result.Should().BeTrue();
    }

    [Fact]
    public void VerifyPassword_should_ReturnFalse_when_GivenPasswordIsInvalid()
    {
        // Arrange
        string password = "##Pass!@word#123098#@43";
        string invalidPassword = "OtherPassword123!@@#";

        // Act
        string hashedPassword = _passwordHasher.HashPassword(password);
        bool result = _passwordHasher.VerifyPassword(invalidPassword, hashedPassword);

        // Assert
        result.Should().BeFalse();
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    public void VerifyPassword_should_ThrowArgumentNullException_when_GivenPasswordIsEmpty(string password)
        => _passwordHasher.Invoking(x => x.VerifyPassword(password, "PasswordHashIsNotEmpty"))
                          .Should()
                          .Throw<ArgumentNullException>();

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    public void VerifyPassword_should_ThrowArgumentNullException_when_GivenPasswordHashIsEmpty(string passwordHash)
        => _passwordHasher.Invoking(x => x.VerifyPassword("PasswordIsNotEmpty", passwordHash))
                          .Should()
                          .Throw<ArgumentNullException>();
}