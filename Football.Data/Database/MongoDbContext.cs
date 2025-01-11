using MongoDB.Driver;

namespace Football.Data.Database;

public class MongoDbContext
{
    private readonly IMongoDatabase _database;

    public MongoDbContext(string connectionString, string databaseName)
    {
        var client = new MongoClient(connectionString);
        _database = client.GetDatabase(databaseName);
    }

    public IMongoCollection<T> GetCollection<T>(string name)
    {
        return _database.GetCollection<T>(name);
    }

    public void CheckCollectionExists(string name)
    {
        var collections = _database.ListCollectionNames().ToList();
        if (!collections.Contains(name))
        {
            _database.CreateCollection(name);
        }
    }
}

