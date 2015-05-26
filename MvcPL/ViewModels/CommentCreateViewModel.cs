using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MvcPL.ViewModels
{
    public class CommentCreateViewModel
    {
        public int TopicId { get; set; }

        [DataType(DataType.MultilineText)]
        public string CommentBody { get; set; }
    }
}