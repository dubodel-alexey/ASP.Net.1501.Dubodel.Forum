using System;
using System.Collections.Generic;
using System.Linq;
using Bll.Interface;
using Bll.Interface.Entities;
using BLL.Mappers;
using DAL.Interface;
using DAL.Interface.Entities;

namespace BLL
{
    public class TopicService : ITopicService
    {
        private readonly IUnitOfWork uow;
        private readonly IRepository<DalTopic> topicRepository;
        private readonly ICommentService commentService;

        public TopicService(IUnitOfWork uow, IRepository<DalTopic> topicRepository, ICommentService commentService)
        {
            this.uow = uow;
            this.topicRepository = topicRepository;
            this.commentService = commentService;
        }

        public int GetLikesCount(BllTopic topic)
        {
            BllComment baseComment = commentService.GetFirstInTopic(topic);
            return commentService.GetLikesCount(baseComment);
        }

        public bool IsLiked(BllUser user, BllTopic topic)
        {
            BllComment baseComment = commentService.GetFirstInTopic(topic);
            return commentService.IsLiked(user, baseComment);
        }

        public void SetLike(BllUser user, BllTopic topic)
        {
            BllComment baseComment = commentService.GetFirstInTopic(topic);
            commentService.SetLike(user, baseComment);
        }

        public void RemoveLike(BllUser user, BllTopic topic)
        {
            BllComment baseComment = commentService.GetFirstInTopic(topic);
            commentService.RemoveLike(user, baseComment);
        }

        public void Update(BllTopic topic)
        {
            topicRepository.Update(topic.ToDalEntity());
            uow.Commit();
        }

        public void Create(BllTopic topic, BllComment comment)
        {
            int id = topicRepository.Add(topic.ToDalEntity());
            comment.TopicId = id;
            uow.Commit();
            commentService.Create(comment);
        }

        public void Delete(BllTopic topic)
        {
            Delete(topic.Id);
        }

        public void Delete(int topicId)
        {
            var comments = commentService.GetAllByTopic(GetById(topicId));
            foreach (var bllComment in comments)
            {
                commentService.Delete(bllComment);
            }

            topicRepository.Delete(topicId);
            uow.Commit();
        }

        public BllTopic GetById(int id)
        {
            return topicRepository.GetById(id).ToBllEntity();
        }

        public IEnumerable<BllTopic> GetByCategory(BllCategory category, int skipCount, int takeCount)
        {
            return
                topicRepository.GetByPredicate(topic => topic.CategoryId == category.Id).Skip(skipCount).Take(takeCount)
                    .Select(topic => topic.ToBllEntity());
            //make ITopicRepository 
        }

        public IEnumerable<BllTopic> GetAllByCategory(BllCategory category)
        {
            return
                topicRepository.GetByPredicate(topic => topic.CategoryId == category.Id)
                    .Select(topic => topic.ToBllEntity());
        }
    }
}
