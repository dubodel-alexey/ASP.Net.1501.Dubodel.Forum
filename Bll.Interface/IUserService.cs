using System.Collections.Generic;
using Bll.Interface.Entities;

namespace Bll.Interface
{
    public interface IUserService
    {
        IEnumerable<BllUser> GetAllUsers();
        IEnumerable<BllUser> GetUsersByRole(BllRole role);
        BllUser GetByLogin(string login);
        BllUser GetById(int userId);
        void Create(BllUser user);
        void Update(BllUser user);
        void Delete(BllUser user);
        void Delete(int userId);
        bool IsExists(string login);
    }
}
