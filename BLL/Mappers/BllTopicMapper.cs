using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bll.Interface.Entities;
using DAL.Interface.Entities;

namespace BLL.Mappers
{
    public static class BllTopicMapper
    {
        public static DalTopic ToDalEntity(this BllTopic ormTopic)
        {
            return new DalTopic
            {
                Id = ormTopic.Id,
                Name = ormTopic.Name,
                CategoryId = ormTopic.CategoryId,
                ForRegistered = ormTopic.ForRegistered
            };
        }

        public static BllTopic ToBllEntity(this DalTopic dalTopic)
        {
            return new BllTopic
            {
                Id = dalTopic.Id,
                Name = dalTopic.Name,
                CategoryId = dalTopic.CategoryId,
                ForRegistered = dalTopic.ForRegistered
            };
        }
    }
}
