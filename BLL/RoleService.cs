using System;
using System.Collections.Generic;
using System.Linq;
using Bll.Interface;
using Bll.Interface.Entities;
using BLL.Mappers;
using DAL.Interface;
using DAL.Interface.Entities;

namespace BLL
{
    public class RoleService : IRoleService
    {
        private readonly IUnitOfWork uow;
        private IRepository<DalRole> roleRepository;
        public RoleService(IUnitOfWork uow, IRepository<DalRole> roleRepository)
        {
            this.uow = uow;
            this.roleRepository = roleRepository;
        }
        public IEnumerable<BllRole> GetAllRoles()
        {
            return roleRepository.GetAll().Select(role => role.ToBllEntity());
        }

        public BllRole GetById(int roleId)
        {
            return roleRepository.GetById(roleId).ToBllEntity();
        }

        public BllRole GetUserRole(BllUser user)
        {
            return GetById(user.RoleId);
        }

        public BllRole GetByName(string roleName)
        {
            return roleRepository.GetByPredicate(role => role.Name == roleName).Select(role => role.ToBllEntity()).FirstOrDefault();
        }
    }
}
