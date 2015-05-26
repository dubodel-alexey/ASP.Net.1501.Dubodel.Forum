using System.Collections.Generic;
using Bll.Interface.Entities;

namespace Bll.Interface
{
    public interface ICategoryService
    {
        BllCategory GetById(int id);
        IEnumerable<BllCategory> GetCategories(BllCategory parentCategory);
        IEnumerable<BllCategory> GetRootCategories();
        BllCategory GetParentCategory(BllCategory category);
        void Update(BllCategory category);
        void Create(BllCategory category);
        void Delete(BllCategory category);
        void Delete(int categoryId);
    }
}
