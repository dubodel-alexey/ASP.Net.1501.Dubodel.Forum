using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using DAL.Interface;
using DAL.Interface.Entities;
using DAl.Mappers;
using ORM;

namespace DAl.Repositories
{
    public class CommentRepository : IRepository<DalComment>
    {
        private readonly DbContext context;

        public CommentRepository(DbContext context)
        {
            this.context = context;
        }

        public IEnumerable<DalComment> GetAll()
        {
            return context.Set<Comment>().Select(comment => new DalComment
            {
                Id = comment.Id,
                Body = comment.Body,
                Time = comment.Time,
                TopicId = comment.TopicId,
                UserId = comment.UserId
            }).ToList();
        }

        public DalComment GetById(int key)
        {
            var ormComment = context.Set<Comment>().FirstOrDefault(comment => comment.Id == key);
            return ormComment.ToDalEntity();
        }

        public IEnumerable<DalComment> GetByPredicate(Expression<Func<DalComment, bool>> filter)
        {
            var replacer = new ParameterTypeVisitor<DalComment, Comment>(filter);
            var exp1 = replacer.Convert();

            return context.Set<Comment>().Where(exp1).Select(comment => new DalComment
            {
                Id = comment.Id,
                Body = comment.Body,
                Time = comment.Time,
                TopicId = comment.TopicId,
                UserId = comment.UserId
            }).ToList();
        }

        public int Add(DalComment dalComment)
        {
            Comment comment = dalComment.ToOrmEntity();
            context.Set<Comment>().Add(comment);
            context.SaveChanges(); // resolve
            return comment.Id;
        }

        public void Delete(DalComment dalComment)
        {
            context.Set<Comment>().Remove(dalComment.ToOrmEntity());
        }

        public void Delete(int commentId)
        {
            Comment commentForDelete = context.Set<Comment>().Find(commentId);
            context.Set<Comment>().Remove(commentForDelete);
        }

        public void Update(DalComment dalComment)
        {
            Comment ormComment = dalComment.ToOrmEntity();
            context.Set<Comment>().Attach(ormComment);
            context.Entry(ormComment).State = EntityState.Modified;
        }
    }
}
