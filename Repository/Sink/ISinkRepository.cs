using IsIoTWeb.Models;
using MongoDB.Bson;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IsIoTWeb.Repository
{
    public interface ISinkRepository : IBaseRepository<Sink>
    {
        public Task<string> GetIdByUser(User user);
    }
}
