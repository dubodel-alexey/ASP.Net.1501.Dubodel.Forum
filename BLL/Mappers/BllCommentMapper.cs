using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bll.Interface.Entities;
using DAL.Interface.Entities;

namespace BLL.Mappers
{
   public static class BllCommentMapper
    {
        public static DalComment ToDalEntity(this BllComment ormComment)
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

        public static BllComment ToBllEntity(this DalComment dalComment)
        {
            return new BllComment
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
