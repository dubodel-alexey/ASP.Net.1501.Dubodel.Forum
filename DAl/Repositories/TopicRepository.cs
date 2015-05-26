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
    public class TopicRepository : IRepository<DalTopic>
    {
        private readonly DbContext context;

        public TopicRepository(DbContext context)
        {
            this.context = context;
        }

        public IEnumerable<DalTopic> GetAll()
        {
            return context.Set<Topic>().Select(topic => new DalTopic
            {
                Id = topic.Id,
                Name = topic.Name,
                CategoryId = topic.CategoryId,
                ForRegistered = topic.ForRegistered
            }).ToList();//(user => user.ToDalUser());
        }

        public DalTopic GetById(int key)
        {
            var ormTopic = context.Set<Topic>().FirstOrDefault(topic => topic.Id == key);
            return ormTopic.ToDalEntity();
        }

        public IEnumerable<DalTopic> GetByPredicate(Expression<Func<DalTopic, bool>> filter)
        {
            var replacer = new ParameterTypeVisitor<DalTopic, Topic>(filter);
            var exp1 = replacer.Convert();

            return context.Set<Topic>().Where(exp1).Select(topic => new DalTopic
            {
                Id = topic.Id,
                Name = topic.Name,
                CategoryId = topic.CategoryId,
                ForRegistered = topic.ForRegistered
            }).ToList();
        }

        public int Add(DalTopic dalTopic)
        {
            Topic topic = dalTopic.ToOrmEntity();
            context.Set<Topic>().Add(topic);
            context.SaveChanges();// resolve!!!
            return topic.Id;
        }

        public void Delete(DalTopic dalTopic)
        {
            Delete(dalTopic.Id);
        }

        public void Delete(int topicId)
        {
            Topic topicForDelete = context.Set<Topic>().Find(topicId);
            context.Set<Topic>().Remove(topicForDelete);
        }

        public void Update(DalTopic dalTopic)
        {
            Topic ormTopic = dalTopic.ToOrmEntity();
            context.Set<Topic>().Attach(ormTopic);
            context.Entry(ormTopic).State = EntityState.Modified;
        }
    }
}
