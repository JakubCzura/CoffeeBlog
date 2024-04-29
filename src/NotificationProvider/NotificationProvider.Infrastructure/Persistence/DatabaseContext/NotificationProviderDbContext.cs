using Microsoft.Extensions.Options;
using MongoDB.Driver;
using NotificationProvider.Domain.SettingsOptions.Database;

namespace NotificationProvider.Infrastructure.Persistence.DatabaseContext;

public class NotificationProviderDbContext
{
    private readonly DatabaseOptions _databaseOptions;
    private readonly MongoClient _mongoClient;
    private readonly IMongoDatabase _database;

    public NotificationProviderDbContext(IOptions<DatabaseOptions> databaseOptions)
    {
        _databaseOptions = databaseOptions.Value;
        _mongoClient = new(_databaseOptions.ConnectionString);
        _database = _mongoClient.GetDatabase(_databaseOptions.DatabaseName);
    }

    public IMongoCollection<T> GetCollection<T>(string collectionName) 
        => _database.GetCollection<T>(collectionName);
}