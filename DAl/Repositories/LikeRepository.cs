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
    public class LikeRepository : IRepository<DalLike>
    {
        private readonly DbContext context;

        public LikeRepository(DbContext context)
        {
            this.context = context;
        }

        public IEnumerable<DalLike> GetAll()
        {
            return context.Set<Like>().Select(like => new DalLike
            {
                Id = like.Id,
                UserId = like.UserId,
                CommentId = like.CommentId
            }).ToList();
        }

        public DalLike GetById(int key)
        {
            var ormLike = context.Set<Like>().FirstOrDefault(like => like.Id == key);
            return ormLike.ToDalEntity();
        }

        public IEnumerable<DalLike> GetByPredicate(Expression<Func<DalLike, bool>> filter)
        {
            var replacer = new ParameterTypeVisitor<DalLike, Like>(filter);
            var exp1 = replacer.Convert();

            return context.Set<Like>().Where(exp1).Select(like => new DalLike
            {
                Id = like.Id,
                UserId = like.UserId,
                CommentId = like.CommentId
            }).ToList();
        }

        public int Add(DalLike dalLike)
        {
            Like like = dalLike.ToOrmEntity();
            context.Set<Like>().Add(like);
            context.SaveChanges();
            return like.Id;
        }

        public void Delete(DalLike dalLike)
        {
            context.Set<Like>().Remove(dalLike.ToOrmEntity());
        }

        public void Delete(int likeId)
        {
            Like likeForDelete = context.Set<Like>().Find(likeId);
            context.Set<Like>().Remove(likeForDelete);
        }

        public void Update(DalLike dalLike)
        {
            Like ormLike = dalLike.ToOrmEntity();
            context.Set<Like>().Attach(ormLike);
            context.Entry(ormLike).State = EntityState.Modified;
        }
    }
}
