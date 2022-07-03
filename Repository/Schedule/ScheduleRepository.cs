using IsIoTWeb.Context;
using IsIoTWeb.Models.Schedule;

namespace IsIoTWeb.Repository
{
    public class ScheduleRepository : BaseRepository<Schedule>, IScheduleRepository
    {
        private const string CollectionName = "schedules";

        public ScheduleRepository(IMongoDbContext context) : base(context, CollectionName)
        {
        }
    }
}
