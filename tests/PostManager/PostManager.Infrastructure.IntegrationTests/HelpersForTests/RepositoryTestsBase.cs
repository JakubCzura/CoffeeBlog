namespace PostManager.Infrastructure.IntegrationTests.HelpersForTests;

[Collection(TestConstants.CollectionName)]
public class RepositoryTestsBase(TestDatabaseFixture testDatabaseFixture) : IAsyncLifetime
{
    public async Task InitializeAsync()
    {
        testDatabaseFixture.PostManagerDbContext.ChangeTracker.Clear();
        await Task.CompletedTask;
    }

    public async Task DisposeAsync()
        => await testDatabaseFixture.ResetDatabaseAsync();
}