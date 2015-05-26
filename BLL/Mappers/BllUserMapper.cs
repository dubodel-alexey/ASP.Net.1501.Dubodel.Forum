using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bll.Interface;
using Bll.Interface.Entities;
using DAL.Interface;
using DAL.Interface.Entities;

namespace BLL.Mappers
{
    public static class BllUserMapper
    {
        public static DalUser ToDalUser(this BllUser bllUserEntity)
        {
            return new DalUser
            {
                Id = bllUserEntity.Id,
                Login = bllUserEntity.Login,
                Password = bllUserEntity.Password,
                RegistrationDateTime = bllUserEntity.RegistrationDateTime,
                RoleId = bllUserEntity.RoleId
            };
        }

        public static BllUser ToBllUser(this DalUser dalUser)
        {
            return new BllUser
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
