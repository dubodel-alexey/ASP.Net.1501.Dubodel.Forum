using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcPL.ViewModels
{
    public class TopicViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public CategoryHeadViewModel ParentCategory { get; set; }
    }
}