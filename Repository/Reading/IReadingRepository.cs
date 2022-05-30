using IsIoTWeb.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IsIoTWeb.Repository
{
    public interface IReadingRepository : IBaseRepository<Reading>
    {
        Task<IEnumerable<Reading>> GetAllByFilter(ReadingFilter filter);
    }
}
