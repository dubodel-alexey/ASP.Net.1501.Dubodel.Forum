using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bll.Interface.Entities;
using DAL.Interface.Entities;

namespace BLL.Mappers
{
 public  static class BllCategoryMapper
    {
     public static DalCategory ToDalEntity(this BllCategory ormCategory)
        {
            return new DalCategory
            {
                Id = ormCategory.Id,
                Name = ormCategory.Name,
                ForRegistered = ormCategory.ForRegistered,
                ParentCategoryId = ormCategory.ParentCategoryId
            };
        }

     public static BllCategory ToBllEntity(this DalCategory dalCategory)
        {
            return new BllCategory
            {
                Id = dalCategory.Id,
                Name = dalCategory.Name,
                ForRegistered = dalCategory.ForRegistered,
                ParentCategoryId = dalCategory.ParentCategoryId
            };
        }
    }
}
