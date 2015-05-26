using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bll.Interface.Entities;

namespace Bll.Interface
{
    public interface ICommentService
    {
        int GetLikesCount(BllComment comment);
        bool IsLiked(BllUser user, BllComment comment);
        void SetLike(BllUser user, BllComment comment);
        void RemoveLike(BllUser user, BllComment comment);
        void Update(BllComment comment);
        void Create(BllComment comment);
        void Delete(BllComment comment);
        void Delete(int commentId);
        BllComment GetById(int commentId);
        IEnumerable<BllComment> GetByTopic(BllTopic topic, int skip, int take);
        IEnumerable<BllComment> GetAllByTopic(BllTopic topic);
        BllComment GetFirstInTopic(BllTopic topic);

    }
}
