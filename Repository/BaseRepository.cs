using IsIoTWeb.Context;
using MongoDB.Bson;
using MongoDB.Driver;
using ServiceStack;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IsIoTWeb.Repository
{
    public abstract class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
    {
        protected readonly IMongoDbContext _context;
        protected IMongoCollection<TEntity> _dbSet;
        
        protected BaseRepository(IMongoDbContext context)
        {
            _context = context;
            // TEntity = Reading => collection named "readings"
            _dbSet = _context.GetCollection<TEntity>(typeof(TEntity).Name.ToLower() + "s");
        }

        public virtual async Task Create(TEntity obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException(typeof(TEntity).Name + " is null!");
            }
            await _dbSet.InsertOneAsync(obj);
        }

        public virtual async Task Delete(string id)
        {
            await _dbSet.DeleteOneAsync(Builders<TEntity>.Filter.Eq("_id", id));
        }

        public virtual async Task Update(TEntity obj)
        {
            await _dbSet.ReplaceOneAsync(Builders<TEntity>.Filter.Eq("_id", obj.GetId()), obj);
        }

        public virtual async Task<TEntity> Get(string id)
        {
            var objectId = new ObjectId(id);
            FilterDefinition<TEntity> filter = Builders<TEntity>.Filter.Eq("_id", objectId);
            return await _dbSet.FindAsync(filter).Result.FirstOrDefaultAsync();
        }

        public virtual async Task<IEnumerable<TEntity>> GetAll()
        {
            var all = await _dbSet.FindAsync(Builders<TEntity>.Filter.Empty);
            return await all.ToListAsync();
        }
    }
}
