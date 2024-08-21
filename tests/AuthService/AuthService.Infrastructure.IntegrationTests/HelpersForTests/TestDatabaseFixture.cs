using AuthService.Infrastructure.Persistence.DatabaseContext;
using Microsoft.EntityFrameworkCore;
using Respawn;
using System.Data.Common;

namespace AuthService.Infrastructure.IntegrationTests.HelpersForTests;

public class TestDatabaseFixture : IAsyncLifetime
{
    private const string _connectionString = $"Server=(localdb)\\MSSQLLocalDB;Database=CoffeeBlogAuthServiceInfrastructureIntegrationTests;Integrated Security=True;TrustServerCertificate=True";
    private Respawner _respawner = default!;
    private DbConnection _connection = default!;

    public AuthServiceDbContext AuthServiceDbContext { get; init; }

    public TestDatabaseFixture()
    {
        DbContextOptions<AuthServiceDbContext> options = new DbContextOptionsBuilder<AuthServiceDbContext>().UseSqlServer(_connectionString).Options;
        AuthServiceDbContext = new(options);
    }

    public async Task DisposeAsync()
    {
        await AuthServiceDbContext.Database.EnsureDeletedAsync();
        await AuthServiceDbContext.DisposeAsync();
        await _connection.CloseAsync();
    }

    public async Task ResetDatabaseAsync()
        => await _respawner.ResetAsync(_connection);

    public async Task InitializeAsync()
    {
        await AuthServiceDbContext.Database.EnsureDeletedAsync();
        await AuthServiceDbContext.Database.EnsureCreatedAsync();

        RespawnerOptions respawnerOptions = new()
        {
            DbAdapter = DbAdapter.SqlServer,
            WithReseed = true
        };

        _connection = AuthServiceDbContext.Database.GetDbConnection();
        await _connection.OpenAsync();
        _respawner = await Respawner.CreateAsync(_connection, respawnerOptions);
    }
}