using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcPL.ViewModels
{
    public class CategoryHeadViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool ForRegistered { get; set; }
    }
}