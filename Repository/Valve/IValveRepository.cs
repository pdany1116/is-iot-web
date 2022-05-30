using IsIoTWeb.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IsIoTWeb.Repository
{
    public interface IValveRepository : IBaseRepository<ValveLog>
    {
        Task<IEnumerable<ValveLog>> GetAllByFilter(ValveLogsFilter filter);
    }
}
