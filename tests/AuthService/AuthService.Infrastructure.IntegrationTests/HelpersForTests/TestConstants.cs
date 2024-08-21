namespace AuthService.Infrastructure.IntegrationTests.HelpersForTests;

public static class TestConstants
{
    public const string CollectionName = nameof(TestDatabaseFixtureCollection);
    public const string ConnectionString = $"Server=(localdb)\\MSSQLLocalDB;Database=AuthServiceInfrastructureIntegrationTests;Integrated Security=True;TrustServerCertificate=True";
}