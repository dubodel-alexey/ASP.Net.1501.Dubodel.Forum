using DAL.Interface.Entities;
using ORM;

namespace DAl.Mappers
{
   public static class DalTopicMapper
    {
        public static DalTopic ToDalEntity(this Topic ormTopic)
        {
            return new DalTopic
            {
                Id = ormTopic.Id,
                Name = ormTopic.Name,
                CategoryId = ormTopic.CategoryId,
                ForRegistered = ormTopic.ForRegistered
            };
        }

        public static Topic ToOrmEntity(this DalTopic dalTopic)
        {
            return new Topic
            {
                Id = dalTopic.Id,
                Name = dalTopic.Name,
                CategoryId = dalTopic.CategoryId,
                ForRegistered = dalTopic.ForRegistered
            };
        }
    }
}
