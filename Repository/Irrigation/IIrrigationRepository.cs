using IsIoTWeb.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IsIoTWeb.Repository
{
    public interface IIrrigationRepository : IBaseRepository<IrrigationLog>
    {
        Task<IEnumerable<IrrigationLog>> GetAllByFilter(IrrigationLogsFilter filter);
    }
}
