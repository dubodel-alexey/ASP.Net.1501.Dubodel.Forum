using DAL.Interface.Entities;
using ORM;

namespace DAl.Mappers
{
    public static class DalUserMapper
    {
        public static DalUser ToDalEntity(this User ormUser)
        {
            return new DalUser
            {
                Id = ormUser.Id,
                Login = ormUser.Login,
                Password = ormUser.Password,
                RoleId = ormUser.RoleId,
                RegistrationDateTime = ormUser.RegistrationDateTime
            };
        }

        public static User ToOrmEntity(this DalUser dalUser)
        {
            return new User
            {
                Id = dalUser.Id,
                Login = dalUser.Login,
                Password = dalUser.Password,
                RegistrationDateTime = dalUser.RegistrationDateTime,
                RoleId = dalUser.RoleId
            };
        }
    }
}
