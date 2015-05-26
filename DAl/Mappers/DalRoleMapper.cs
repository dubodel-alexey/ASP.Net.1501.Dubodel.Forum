using DAL.Interface.Entities;
using ORM;

namespace DAl.Mappers
{
    public static class DalRoleMapper
    {
        public static DalRole ToDalEntity(this Role ormRole)
        {
            return new DalRole
            {
                Id = ormRole.Id,
                Name = ormRole.Name
            };
        }

        public static Role ToOrmEntity(this DalRole dalRole)
        {
            return new Role
            {
                Id = dalRole.Id,
                Name = dalRole.Name,
            };
        }
    }
}
