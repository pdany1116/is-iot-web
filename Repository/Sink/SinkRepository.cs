using IsIoTWeb.Context;
using IsIoTWeb.Models;
using MongoDB.Driver;
using System;
using System.Threading.Tasks;

namespace IsIoTWeb.Repository
{
    public class SinkRepository : BaseRepository<Sink>, ISinkRepository
    {
        private const string CollectionName = "sinks";

        public SinkRepository(IMongoDbContext context) : base(context, CollectionName)
        {
        }

        public async Task<string> GetIdByUser(User user)
        {
            try
            {
                var asyncCursor = await _collection.FindAsync(Builders<Sink>.Filter.Empty);
                var all = await asyncCursor.ToListAsync();
                string userId = Utils.Utils.DynamicObjectIdToString(user.Id.Timestamp, user.Id.Machine, user.Id.Pid, user.Id.Increment);
                var sink = all.Find(x => x.Users.Contains(userId));
                return sink == null ? null : sink.Id;
            }
            catch(System.NullReferenceException)
            {
                return null;
            }
        }
    }
}
