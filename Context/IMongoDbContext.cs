using MongoDB.Driver;

namespace IsIoTWeb.Context
{
    public interface IMongoDbContext
    {
        public IMongoCollection<T> GetCollection<T>(string name);
    }
}