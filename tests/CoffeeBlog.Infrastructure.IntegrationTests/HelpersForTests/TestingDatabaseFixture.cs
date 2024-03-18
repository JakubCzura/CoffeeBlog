using CoffeeBlog.Infrastructure.Persistence.DatabaseContext;
using Microsoft.EntityFrameworkCore;
using Respawn;
using System.Data.Common;

namespace CoffeeBlog.Infrastructure.IntegrationTests.HelpersForTests;

public class TestingDatabaseFixture : IAsyncLifetime
{
    public readonly CoffeeBlogDbContext CoffeeBlogContext;
    private static string CoffeeBlogDbConnectionString => $"Server=(localdb)\\MSSQLLocalDB;Database=PersistenceIntegrationTests;Integrated Security=True;TrustServerCertificate=True";

    private Respawner _respawner = default!;
    private DbConnection _connection = default!;

    public TestingDatabaseFixture()
    {
        DbContextOptions<CoffeeBlogDbContext> options = new DbContextOptionsBuilder<CoffeeBlogDbContext>()
                                                           .UseSqlServer(CoffeeBlogDbConnectionString)
                                                           .Options;
        CoffeeBlogContext = new(options);

        //CoffeeBlogContext.Database.EnsureDeleted();
        //CoffeeBlogContext.Database.EnsureCreated();
    }

    public async Task DisposeAsync()
    {
        //await CoffeeBlogContext.Database.EnsureDeletedAsync();
        await CoffeeBlogContext.DisposeAsync();

        await _connection.CloseAsync();
    }

    public async Task ResetDatabaseAsync() 
        => await _respawner.ResetAsync(_connection);

    public async Task InitializeAsync()
    {
        await CoffeeBlogContext.Database.EnsureDeletedAsync();
        await CoffeeBlogContext.Database.EnsureCreatedAsync();

        RespawnerOptions respawnerOptions = new()
        {
            DbAdapter = DbAdapter.SqlServer
        };

        _connection = CoffeeBlogContext.Database.GetDbConnection();
        await _connection.OpenAsync();
        _respawner = await Respawner.CreateAsync(_connection, respawnerOptions);  
    }
}