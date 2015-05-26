using DAL.Interface.Entities;
using ORM;

namespace DAl.Mappers
{
    public static class DalLikeMapper
    {
        public static DalLike ToDalEntity(this Like ormLike)
        {
            return new DalLike
            {
                Id = ormLike.Id,
                CommentId = ormLike.CommentId,
                UserId = ormLike.UserId
            };
        }

        public static Like ToOrmEntity(this DalLike dalLike)
        {
            return new Like
            {
                Id = dalLike.Id,
                CommentId = dalLike.CommentId,
                UserId = dalLike.UserId
            };
        }
    }
}
