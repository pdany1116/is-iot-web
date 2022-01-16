using IsIoTWeb.Context;
using IsIoTWeb.Models;

namespace IsIoTWeb.Repository
{
    public class ReadingRepository : BaseRepository<Reading>, IReadingRepository
    {
        public ReadingRepository(IMongoDbContext context) : base(context)
        {
        }
    }
}
