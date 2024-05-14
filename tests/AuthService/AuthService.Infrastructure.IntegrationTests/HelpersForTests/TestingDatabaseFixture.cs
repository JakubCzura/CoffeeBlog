using AuthService.Infrastructure.Persistence.DatabaseContext;
using Microsoft.EntityFrameworkCore;
using Respawn;
using System.Data.Common;

namespace AuthService.Infrastructure.IntegrationTests.HelpersForTests;

public class TestingDatabaseFixture : IAsyncLifetime
{
    public readonly AuthServiceDbContext AuthServiceDbContext;
    private static string CoffeeBlogAuthServiceInfrastructureDbConnectionString => $"Server=(localdb)\\MSSQLLocalDB;Database=CoffeeBlogAuthServiceInfrastructureIntegrationTests;Integrated Security=True;TrustServerCertificate=True";

    private Respawner _respawner = default!;
    private DbConnection _connection = default!;

    public TestingDatabaseFixture()
    {
        DbContextOptions<AuthServiceDbContext> options = new DbContextOptionsBuilder<AuthServiceDbContext>()
                                                           .UseSqlServer(CoffeeBlogAuthServiceInfrastructureDbConnectionString)
                                                           .Options;
        AuthServiceDbContext = new(options);

        //AuthServiceDbContext.Database.EnsureDeleted();
        //AuthServiceDbContext.Database.EnsureCreated();
    }

    public async Task DisposeAsync()
    {
        //await AuthServiceDbContext.Database.EnsureDeletedAsync();
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
            DbAdapter = DbAdapter.SqlServer
        };

        _connection = AuthServiceDbContext.Database.GetDbConnection();
        await _connection.OpenAsync();
        _respawner = await Respawner.CreateAsync(_connection, respawnerOptions);
    }
}