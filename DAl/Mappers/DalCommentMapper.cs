using DAL.Interface.Entities;
using ORM;

namespace DAl.Mappers
{
    public static class DalCommentMapper
    {
        public static DalComment ToDalEntity(this Comment ormComment)
        {
            return new DalComment
            {
                Id = ormComment.Id,
                Body = ormComment.Body,
                Time = ormComment.Time,
                TopicId = ormComment.TopicId,
                UserId = ormComment.UserId
            };
        }

        public static Comment ToOrmEntity(this DalComment dalComment)
        {
            return new Comment
            {
                Id = dalComment.Id,
                Body = dalComment.Body,
                Time = dalComment.Time,
                TopicId = dalComment.TopicId,
                UserId = dalComment.UserId
            };
        }
    }
}
