using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcPL.ViewModels
{
    public class TopicCreateViewModel
    {
        public string Name { get; set; }
        public string CommentBody { get; set; }
        public int ParentCategoryId { get; set; }
    }
}