using IsIoTWeb.Context;
using IsIoTWeb.Models;

namespace IsIoTWeb.Repository
{
    public class ValveRepository : BaseRepository<Valve>, IValveRepository
    {
        private const string CollectionName = "valves";

        public ValveRepository(IMongoDbContext context) : base(context, CollectionName)
        {
        }
    }
}
