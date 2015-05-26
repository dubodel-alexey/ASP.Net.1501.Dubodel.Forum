using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bll.Interface;
using Bll.Interface.Entities;
using BLL.Mappers;
using DAL.Interface;
using DAL.Interface.Entities;

namespace BLL
{
    public class CommentService : ICommentService
    {
        private readonly IUnitOfWork uow;
        private IRepository<DalComment> commentRepository;
        private IRepository<DalLike> likeRepository;
        public CommentService(IUnitOfWork uow, IRepository<DalComment> commentRepository, IRepository<DalLike> likeRepository)
        {
            this.uow = uow;
            this.commentRepository = commentRepository;
            this.likeRepository = likeRepository;
        }


        public int GetLikesCount(BllComment comment)
        {
            return likeRepository.GetByPredicate(like => like.CommentId == comment.Id).Count();
        }

        public bool IsLiked(BllUser user, BllComment comment)
        {
            return likeRepository.GetByPredicate(like => like.CommentId == comment.Id && like.UserId == user.Id).Any();
        }

        public void SetLike(BllUser user, BllComment comment)
        {
            likeRepository.Add(new DalLike() { CommentId = comment.Id, UserId = user.Id });
        }

        public void RemoveLike(BllUser user, BllComment comment)
        {
            var dalLike = likeRepository.GetByPredicate(like => like.CommentId == comment.Id && like.UserId == user.Id).FirstOrDefault();
            if (dalLike != null)
            {
                likeRepository.Delete(dalLike);
            }
        }

        public void Update(BllComment comment)
        {
            commentRepository.Update(comment.ToDalEntity());
            uow.Commit();
        }

        public void Create(BllComment comment)
        {
            commentRepository.Add(comment.ToDalEntity());
            uow.Commit();
        }

        public void Delete(BllComment comment)
        {
            Delete(comment.Id);
        }   

        public void Delete(int commentId)
        {
            commentRepository.Delete(commentId);
            uow.Commit();
        }

        public BllComment GetById(int commentId)
        {
            return commentRepository.GetById(commentId).ToBllEntity();
        }

        public IEnumerable<BllComment> GetByTopic(BllTopic topic, int skip, int take)
        {//add to repos
            throw new NotImplementedException();
        }

        public IEnumerable<BllComment> GetAllByTopic(BllTopic topic)
        {
            return commentRepository.GetByPredicate(comment => comment.TopicId == topic.Id).Select(x => x.ToBllEntity());
        }

        public BllComment GetFirstInTopic(BllTopic topic)
        {//add to repository
            return
                commentRepository.GetByPredicate(comment => comment.TopicId == topic.Id).FirstOrDefault().ToBllEntity();
        }
    }
}
