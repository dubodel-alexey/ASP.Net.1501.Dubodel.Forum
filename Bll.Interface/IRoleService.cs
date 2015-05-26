using System.Collections.Generic;
using Bll.Interface.Entities;

namespace Bll.Interface
{
    public interface IRoleService
    {
        IEnumerable<BllRole> GetAllRoles();
        BllRole GetById(int roleId);
        BllRole GetUserRole(BllUser user);
        BllRole GetByName(string roleName);
    }
}
