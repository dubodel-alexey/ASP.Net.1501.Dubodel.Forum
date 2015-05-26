using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using DAL.Interface;
using DAL.Interface.Entities;
using DAl.Mappers;
using ORM;

namespace DAl.Repositories
{
    public class UserRepository : IRepository<DalUser>
    {
        private readonly DbContext context;

        public UserRepository(DbContext context)
        {
            this.context = context;
        }

        public IEnumerable<DalUser> GetAll()
        {
            return context.Set<User>().Select(user => new DalUser
            {
                Id = user.Id,
                Login = user.Login,
                Password = user.Password,
                RegistrationDateTime = user.RegistrationDateTime,
                RoleId = user.RoleId
            }).ToList();
        }

        public DalUser GetById(int key)
        {
            var ormUser = context.Set<User>().FirstOrDefault(user => user.Id == key);
            return ormUser.ToDalEntity();
        }

        public IEnumerable<DalUser> GetByPredicate(Expression<Func<DalUser, bool>> filter)
        {
            var replacer = new ParameterTypeVisitor<DalUser, User>(filter);
            var exp1 = replacer.Convert();

            return context.Set<User>().Where(exp1).Select(user => new DalUser
            {
                Id = user.Id,
                Login = user.Login,
                Password = user.Password,
                RegistrationDateTime = user.RegistrationDateTime,
                RoleId = user.RoleId
            }).ToList();
        }

        public int Add(DalUser dalUser)
        {
            var user = dalUser.ToOrmEntity();
            context.Set<User>().Add(user);
            context.SaveChanges();
            return user.Id;
        }

        public void Delete(DalUser dalUser)
        {
            context.Set<User>().Remove(dalUser.ToOrmEntity());
        }

        public void Delete(int userId)
        {
            User userForDelete = context.Set<User>().Find(userId);
            context.Set<User>().Remove(userForDelete);
        }

        public void Update(DalUser dalUser)
        {
            User ormUser = dalUser.ToOrmEntity();
            context.Set<User>().Attach(ormUser);
            context.Entry(ormUser).State = EntityState.Modified;
        }
    }
}
