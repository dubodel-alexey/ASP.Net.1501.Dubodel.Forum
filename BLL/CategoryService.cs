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
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork uow;
        private IRepository<DalCategory> categoryRepository;
        private ITopicService topicService;

        public CategoryService(IUnitOfWork uow, IRepository<DalCategory> categoryRepository, ITopicService topicService)
        {
            this.uow = uow;
            this.categoryRepository = categoryRepository;
            this.topicService = topicService;
        }

        public BllCategory GetById(int id)
        {
            return categoryRepository.GetById(id).ToBllEntity();
        }

        public IEnumerable<BllCategory> GetCategories(BllCategory parentCategory)
        {
            if (parentCategory == null)
                return categoryRepository.GetByPredicate(x => x.ParentCategoryId == null).Select(x => x.ToBllEntity());
            return categoryRepository.GetByPredicate(x => x.ParentCategoryId == parentCategory.Id).Select(x => x.ToBllEntity());
        }

        public IEnumerable<BllCategory> GetRootCategories()
        {
            return GetCategories(null);
        }

        public BllCategory GetParentCategory(BllCategory category)
        {
            return category.ParentCategoryId.HasValue ? GetById(category.ParentCategoryId.Value) : null;
        }

        public void Update(BllCategory category)
        {
            categoryRepository.Update(category.ToDalEntity());
            uow.Commit();
        }

        public void Create(BllCategory category)
        {
            categoryRepository.Add(category.ToDalEntity());
            uow.Commit();
        }

        public void Delete(BllCategory category)
        {
            var topics = topicService.GetAllByCategory(category);
            foreach (var bllTopic in topics)
            {
                topicService.Delete(bllTopic);
            }
            var categories = GetCategories(category);
            foreach (var bllCategory in categories)
            {
                Delete(bllCategory);
            }
            categoryRepository.Delete(category.ToDalEntity());
            uow.Commit();
        }

        public void Delete(int categoryId)
        {
            Delete(GetById(categoryId));
        }
    }
}
