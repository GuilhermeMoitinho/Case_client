using CaseClient.Domain.Entities;
using MongoDB.Driver;

namespace CaseClient.Data.CustomerContext;

public class MongoDbContext
{
    private readonly IMongoDatabase _database;

    public MongoDbContext(string connectionString, string databaseName)
    {
        var client = new MongoClient(connectionString);
        _database = client.GetDatabase(databaseName);
    }

    public IMongoCollection<Customer> Customers => _database.GetCollection<Customer>("Customers");
}