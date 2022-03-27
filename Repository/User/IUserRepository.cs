using IsIoTWeb.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IsIoTWeb.Repository
{
    public interface IUserRepository : IBaseRepository<User>
    {
        Task<User> GetLoggedUserByUsername(string username);
        Task<List<string>> Create(UserInputModel userInputModel);
        Task<List<string>> Update(UserInputModel userInputModel);
    }
}
