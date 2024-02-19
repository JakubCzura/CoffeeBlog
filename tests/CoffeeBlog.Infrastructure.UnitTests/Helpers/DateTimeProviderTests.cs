using CoffeeBlog.Infrastructure.Helpers;
using FluentAssertions;

namespace CoffeeBlog.Infrastructure.UnitTests.Helpers;

public class DateTimeProviderTests
{
    private readonly DateTimeProvider _dateTimeProvider = new();

    [Fact]
    public void UtcNow_should_ReturnCurrentUtcTime()
        => _dateTimeProvider.UtcNow.Should()
                                   .BeCloseTo(DateTime.UtcNow, new TimeSpan(0, 0, 0, 0, 1));

    [Fact]
    public void Now_should_ReturnCurrentTime()
        => _dateTimeProvider.Now.Should()
                                .BeCloseTo(DateTime.Now, new TimeSpan(0, 0, 0, 0, 1));
}