using IsIoTWeb.Context;
using IsIoTWeb.Models;

namespace IsIoTWeb.Repository
{
    public class IrrigationRepository : BaseRepository<IrrigationLog>, IIrrigationRepository
    {
        private const string CollectionName = "irrigations";

        public IrrigationRepository(IMongoDbContext context) : base(context, CollectionName)
        {
        }
    }
}
