﻿using CoffeeBlog.Application.ExtensionMethods.Collections;
using FluentAssertions;

namespace CoffeeBlog.Application.UnitTests.ExtensionMethods.Collections;

public class IEnumerableExtensionTests
{
    public static IEnumerable<object[]> IsAnyElementNullOrWhiteSpace_Data()
    {
        yield return new object[] { new string?[] { "", "b", "c", "d", "e" } };
        yield return new object[] { new string?[] { "a", "b", "c", " ", "e" } };
        yield return new object[] { new string?[] { "a", "b", "c", null, "e" } };
    }

    [Theory]
    [MemberData(nameof(IsAnyElementNullOrWhiteSpace_Data))]
    public void IsAnyElementNullOrWhiteSpace_should_ReturnTrue_when_AnyElementIsNullOrWhiteSpace(IEnumerable<string> collection)
    {
        // Act
        bool result = collection.IsAnyElementNullOrWhiteSpace();

        // Assert
        result.Should().BeTrue();
    }

    [Fact]
    public void IsAnyElementNullOrWhiteSpace_should_ReturnFalse_when_NoElementIsNullOrWhiteSpace()
    {
        // Arrange
        IEnumerable<string> collection = ["a", "b", "c", "d", "e"];

        // Act
        bool result = collection.IsAnyElementNullOrWhiteSpace();

        // Assert
        result.Should().BeFalse();
    }

    [Fact]
    public void IsAnyElementNullOrWhiteSpace_should_ReturnFalse_when_CollectionIsEmpty()
    {
        // Arrange
        IEnumerable<string> collection = [];

        // Act
        bool result = collection.IsAnyElementNullOrWhiteSpace();

        // Assert
        result.Should().BeFalse();
    }

    [Fact]
    public void IsAnyElementNullOrWhiteSpace_should_ThrowArgumentNullException_when_CollectionIsNull()
    {
        // Arrange
        IEnumerable<string> collection = null!;

        // Act
        Action action = () => collection.IsAnyElementNullOrWhiteSpace();

        // Assert
        action.Should().Throw<ArgumentNullException>();
    }
}