using DAL.Interface.Entities;
using ORM;

namespace DAl.Mappers
{
    public static class DalCategoryMapper
    {
        public static DalCategory ToDalEntity(this Category ormTopic)
        {
            return new DalCategory
            {
                Id = ormTopic.Id,
                Name = ormTopic.Name,
                ForRegistered = ormTopic.ForRegistered,
                ParentCategoryId = ormTopic.ParentCategoryId
            };
        }

        public static Category ToOrmEntity(this DalCategory dalTopic)
        {
            return new Category
            {
                Id = dalTopic.Id,
                Name = dalTopic.Name,
                ForRegistered = dalTopic.ForRegistered,
                ParentCategoryId = dalTopic.ParentCategoryId
            };
        }
    }
}
