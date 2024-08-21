using ArticleManager.Infrastructure.Persistence.DatabaseContext;
using Microsoft.EntityFrameworkCore;
using Respawn;
using System.Data.Common;

namespace ArticleManager.Infrastructure.IntegrationTests.HelpersForTests;

public class TestDatabaseFixture : IAsyncLifetime
{
    private Respawner _respawner = default!;
    private DbConnection _connection = default!;

    public ArticleManagerDbContext ArticleManagerDbContext { get; init; }

    public TestDatabaseFixture()
    {
        DbContextOptions<ArticleManagerDbContext> options = new DbContextOptionsBuilder<ArticleManagerDbContext>().UseSqlServer(TestConstants.ConnectionString).Options;
        ArticleManagerDbContext = new(options);
    }

    public async Task DisposeAsync()
    {
        await ArticleManagerDbContext.Database.EnsureDeletedAsync();
        await ArticleManagerDbContext.DisposeAsync();
        await _connection.CloseAsync();
    }

    public async Task ResetDatabaseAsync()
        => await _respawner.ResetAsync(_connection);

    public async Task InitializeAsync()
    {
        await ArticleManagerDbContext.Database.EnsureDeletedAsync();
        await ArticleManagerDbContext.Database.EnsureCreatedAsync();

        RespawnerOptions respawnerOptions = new()
        {
            DbAdapter = DbAdapter.SqlServer,
            WithReseed = true
        };

        _connection = ArticleManagerDbContext.Database.GetDbConnection();
        await _connection.OpenAsync();
        _respawner = await Respawner.CreateAsync(_connection, respawnerOptions);
    }
}