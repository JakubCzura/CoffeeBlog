namespace CoffeeBlog.Infrastructure.IntegrationTests.HelpersForTests;

[CollectionDefinition("Testing database collection")]
public class TestingDatabaseFixtureCollection : ICollectionFixture<CoffeeBlogDatabaseFixture>
{
}