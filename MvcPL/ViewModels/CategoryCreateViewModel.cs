using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcPL.ViewModels
{
    public class CategoryCreateViewModel
    {
        public string Name { get; set; }
        public int? ParentCategoryId { get; set; }
        public bool ForRegistered { get; set; }
    }
}