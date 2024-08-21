using Microsoft.EntityFrameworkCore;
using PostManager.Infrastructure.Persistence.DatabaseContext;
using Respawn;
using System.Data.Common;

namespace PostManager.Infrastructure.IntegrationTests.HelpersForTests;

public class TestDatabaseFixture : IAsyncLifetime
{
    private Respawner _respawner = default!;
    private DbConnection _connection = default!;

    public PostManagerDbContext PostManagerDbContext { get; init; }

    public TestDatabaseFixture()
    {
        DbContextOptions<PostManagerDbContext> options = new DbContextOptionsBuilder<PostManagerDbContext>().UseSqlServer(TestConstants.ConnectionString).Options;
        PostManagerDbContext = new(options);
    }

    public async Task DisposeAsync()
    {
        await PostManagerDbContext.Database.EnsureDeletedAsync();
        await PostManagerDbContext.DisposeAsync();
        await _connection.CloseAsync();
    }

    public async Task ResetDatabaseAsync()
        => await _respawner.ResetAsync(_connection);

    public async Task InitializeAsync()
    {
        await PostManagerDbContext.Database.EnsureDeletedAsync();
        await PostManagerDbContext.Database.EnsureCreatedAsync();

        RespawnerOptions respawnerOptions = new()
        {
            DbAdapter = DbAdapter.SqlServer,
            WithReseed = true
        };

        _connection = PostManagerDbContext.Database.GetDbConnection();
        await _connection.OpenAsync();
        _respawner = await Respawner.CreateAsync(_connection, respawnerOptions);
    }
}