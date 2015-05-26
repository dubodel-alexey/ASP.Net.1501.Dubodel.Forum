using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bll.Interface.Entities;

namespace Bll.Interface
{
    public interface ITopicService
    {
        int GetLikesCount(BllTopic topic);
        bool IsLiked(BllUser user, BllTopic topic);
        void SetLike(BllUser user, BllTopic topic);
        void RemoveLike(BllUser user, BllTopic topic);
        void Update(BllTopic topic);
        void Create(BllTopic topic, BllComment comment);
        void Delete(BllTopic topic);
        void Delete(int topicId);
        BllTopic GetById(int id);
        IEnumerable<BllTopic> GetByCategory(BllCategory category, int skip, int take);
        IEnumerable<BllTopic> GetAllByCategory(BllCategory category);
    }
}
