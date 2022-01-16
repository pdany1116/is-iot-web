using IsIoTWeb.Settings;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace IsIoTWeb.Context
{
    public class MongoDbContext : IMongoDbContext
    {
        private IMongoDatabase _database;
        private MongoClient _mongoClient;

        public MongoDbContext(IOptions<MongoDbSettings> settings)
        {
            _mongoClient = new MongoClient(settings.Value.ConnectionString);
            _database = _mongoClient.GetDatabase(settings.Value.DatabaseName);
        }

        public IMongoCollection<T> GetCollection<T>(string name)
        {
            return _database.GetCollection<T>(name);
        }
    }
}
