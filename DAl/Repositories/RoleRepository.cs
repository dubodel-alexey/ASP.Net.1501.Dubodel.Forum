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
    public class RoleRepository : IRepository<DalRole>
    {
        private readonly DbContext context;

        public RoleRepository(DbContext context)
        {
            this.context = context;
        }

        public IEnumerable<DalRole> GetAll()
        {
            return context.Set<Role>().Select(role => new DalRole
            {
                Id = role.Id,
                Name = role.Name
            }).ToList();
        }

        public DalRole GetById(int key)
        {
            var ormRole = context.Set<Role>().FirstOrDefault(role => role.Id == key);
            return ormRole.ToDalEntity();
        }

        public IEnumerable<DalRole> GetByPredicate(Expression<Func<DalRole, bool>> filter)
        {
            var replacer = new ParameterTypeVisitor<DalRole, Role>(filter);
            var exp1 = replacer.Convert();

            return context.Set<Role>().Where(exp1).Select(role => new DalRole
            {
                Id = role.Id,
                Name = role.Name
            }).ToList();
        }

        public int Add(DalRole dalRole)
        {
            Role role = dalRole.ToOrmEntity();
            context.Set<Role>().Add(role);
            context.SaveChanges();
            return role.Id;
        }

        public void Delete(DalRole dalRole)
        {
            context.Set<Role>().Remove(dalRole.ToOrmEntity());
        }

        public void Delete(int roleId)
        {
            Role roleForDelete = context.Set<Role>().Find(roleId);
            context.Set<Role>().Remove(roleForDelete);
        }

        public void Update(DalRole dalRole)
        {
            Role ormRole = dalRole.ToOrmEntity();
            context.Set<Role>().Attach(ormRole);
            context.Entry(ormRole).State = EntityState.Modified;
        }
    }
}
