using System.Collections.Generic;

namespace MvcPL.ViewModels
{
    public class CategoryViewModel
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public CategoryHeadViewModel ParentCategory { get; set; }
    }
}