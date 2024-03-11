using CoffeeBlog.Application.ExtensionMethods.String;
using FluentAssertions;

namespace CoffeeBlog.Application.UnitTests.ExtensionMethods.String;

public class StringExtensionsTests
{
    [InlineData("TestStringMessage", "testStringMessage")]
    [InlineData("TestString", "testString")]
    [InlineData("Test", "test")]
    [InlineData("TesT", "tesT")]
    [InlineData("test", "test")]
    [Theory]
    public void ToCamelCase_should_ReturnStringConvertedToCamelCase_when_StringIsSpecified(string value, string expected)
        => value.ToCamelCase().Should().Be(expected);

    [InlineData(null)]
    [InlineData(" ")]
    [InlineData("    ")]
    [Theory]
    public void ToCamelCase_should_ReturnEmptyString_when_StringIsNotSpecified(string? value)
        => value.ToCamelCase().Should().Be(string.Empty);
}