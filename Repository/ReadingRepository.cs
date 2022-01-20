using IsIoTWeb.Context;
using IsIoTWeb.Models;
using System.Threading.Tasks;

namespace IsIoTWeb.Repository
{
    public class ReadingRepository : BaseRepository<Reading>, IReadingRepository
    {
        public ReadingRepository(IMongoDbContext context) : base(context)
        {
        }

        // Override Update method because no Reading should be modified
        public override async Task Update(Reading obj)
        {
            await Task.CompletedTask;
        }

        // Override Update method because no Reading should be modified
        public override async Task Delete(string id)
        {
            await Task.CompletedTask;
        }
    }
}
