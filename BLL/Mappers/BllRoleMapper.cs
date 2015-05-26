using Bll.Interface.Entities;
using DAL.Interface.Entities;

namespace BLL.Mappers
{
    public static class BllRoleMapper
    {
        public static DalRole ToDalEntity(this BllRole ormRole)
        {
            return new DalRole
            {
                Id = ormRole.Id,
                Name = ormRole.Name
            };
        }

        public static BllRole ToBllEntity(this DalRole dalRole)
        {
            return new BllRole
            {
                Id = dalRole.Id,
                Name = dalRole.Name,
            };
        }
    }
}
