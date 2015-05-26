using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORM
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool ForRegistered { get; set; }

        public int? ParentCategoryId { get; set; }
        public virtual Category ParentCategory { get; set; }
    }
}
