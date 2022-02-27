using IsIoTWeb.Context;
using IsIoTWeb.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IsIoTWeb.Repository
{
    public abstract class BaseRepository<TDocument> : IBaseRepository<TDocument> where TDocument : IDocument
    {
        protected readonly IMongoDbContext _context;
        protected IMongoCollection<TDocument> _dbSet;
        
        protected BaseRepository(IMongoDbContext context, string collectionName)
        {
            _context = context;
            _dbSet = _context.GetCollection<TDocument>(collectionName);
        }

        public virtual async Task Create(TDocument obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException(typeof(TDocument).Name + " is null!");
            }
            await _dbSet.InsertOneAsync(obj);
        }

        public virtual async Task Delete(string id)
        {
            await _dbSet.DeleteOneAsync(Builders<TDocument>.Filter.Eq("_id", id));
        }

        public virtual async Task Update(TDocument obj)
        {
            await _dbSet.ReplaceOneAsync(Builders<TDocument>.Filter.Eq("_id", obj.Id), obj);
        }

        public virtual async Task<TDocument> Get(string id)
        {
            var objectId = new ObjectId(id);
            FilterDefinition<TDocument> filter = Builders<TDocument>.Filter.Eq("_id", objectId);
            return await _dbSet.FindAsync(filter).Result.FirstOrDefaultAsync();
        }

        public virtual async Task<IEnumerable<TDocument>> GetAll()
        {
            var all = await _dbSet.FindAsync(Builders<TDocument>.Filter.Empty);
            return await all.ToListAsync();
        }
    }
}
