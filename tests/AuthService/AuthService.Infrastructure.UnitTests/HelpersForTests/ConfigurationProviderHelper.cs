using Microsoft.Extensions.Configuration;

namespace AuthService.Infrastructure.UnitTests.HelpersForTests;

public class ConfigurationProviderHelper
{
    public static IConfiguration InitializeConfiguration()
        => new ConfigurationBuilder().AddJsonFile(Path.Combine("HelpersForTests", "appsettings.tests.json"))
                                     .AddEnvironmentVariables()
                                     .Build();
}