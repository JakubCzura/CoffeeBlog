using Microsoft.Extensions.Configuration;

namespace CoffeeBlog.Infrastructure.UnitTests.HelpersForTests;

public class ConfigurationProviderHelper
{
    public static IConfiguration InitConfiguration()
        => new ConfigurationBuilder().AddJsonFile(@"HelpersForTests\appsettings.tests.json")
                                     .AddEnvironmentVariables()
                                     .Build();
}