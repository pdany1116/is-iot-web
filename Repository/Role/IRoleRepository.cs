using IsIoTWeb.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IsIoTWeb.Repository
{
    public interface IRoleRepository : IBaseRepository<Role>
    {
        Task<List<string>> Create(string roleName);
    }
}
