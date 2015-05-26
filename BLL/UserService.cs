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
    public class UserService : IUserService
    {
        private readonly IUnitOfWork uow;
        private readonly IRepository<DalUser> userRepository;

        public UserService(IUnitOfWork uow, IRepository<DalUser> userRepository)
        {
            this.uow = uow;
            this.userRepository = userRepository;
        }

        public IEnumerable<BllUser> GetAllUsers()
        {
            return userRepository.GetAll().Select(user => user.ToBllUser());
        }

        public IEnumerable<BllUser> GetUsersByRole(BllRole role)
        {
            return userRepository.GetByPredicate(user => user.RoleId == role.Id).Select(user => user.ToBllUser());
        }

        public BllUser GetByLogin(string login)
        {
            return userRepository.GetByPredicate(user => user.Login == login).Select(user => user.ToBllUser()).FirstOrDefault();
        }

        public BllUser GetById(int userId)
        {
            return userRepository.GetById(userId).ToBllUser();
        }

        public void Create(BllUser user)
        {
            userRepository.Add(user.ToDalUser());
            uow.Commit();
        }

        public void Update(BllUser user)
        {
            userRepository.Update(user.ToDalUser());
            uow.Commit();
        }

        public void Delete(BllUser user)
        {
            userRepository.Delete(user.ToDalUser());
            uow.Commit();
        }

        public void Delete(int userId)
        {
            userRepository.Delete(userId);
            uow.Commit();
        }

        public bool IsExists(string login)
        {
            return GetByLogin(login) != null;
        }
    }
}
