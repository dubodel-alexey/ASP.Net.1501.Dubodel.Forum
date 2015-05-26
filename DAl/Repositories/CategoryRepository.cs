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
    public class CategoryRepository : IRepository<DalCategory>
    {
        private readonly DbContext context;

        public CategoryRepository(DbContext context)
        {
            this.context = context;
        }

        public IEnumerable<DalCategory> GetAll()
        {
            return context.Set<Category>().Select(category => new DalCategory
            {
                Id = category.Id,
                ForRegistered = category.ForRegistered,
                Name = category.Name,
                ParentCategoryId = category.ParentCategoryId
            }).ToList();
        }

        public DalCategory GetById(int key)
        {
            var ormCategory = context.Set<Category>().FirstOrDefault(category => category.Id == key);
            return ormCategory.ToDalEntity();
        }

        public IEnumerable<DalCategory> GetByPredicate(Expression<Func<DalCategory, bool>> filter)
        {
            var replacer = new ParameterTypeVisitor<DalCategory, Category>(filter);
            var exp1 = replacer.Convert();

            return context.Set<Category>().Where(exp1).Select(category => new DalCategory
            {
                Id = category.Id,
                ForRegistered = category.ForRegistered,
                Name = category.Name,
                ParentCategoryId = category.ParentCategoryId
            }).ToList();
        }

        public int Add(DalCategory dalCategory)
        {
            Category category = dalCategory.ToOrmEntity();
            context.Set<Category>().Add(category);
            context.SaveChanges();
            return category.Id;
        }

        public void Delete(DalCategory dalCategory)
        {
            Delete(dalCategory.Id);
        }

        public void Delete(int categoryId)
        {
            Category categoryForDelete = context.Set<Category>().Find(categoryId);
            context.Set<Category>().Remove(categoryForDelete);
        }

        public void Update(DalCategory dalCategory)
        {
            Category ormCategory = dalCategory.ToOrmEntity();
            context.Set<Category>().Attach(ormCategory);
            context.Entry(ormCategory).State = EntityState.Modified;
        }
    }
}
