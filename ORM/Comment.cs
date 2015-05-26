﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORM
{
    public class Comment
    {
        public int Id { get; set; }
        public string Body { get; set; }
        public DateTime Time { get; set; }

        public int TopicId { get; set; }
        public int UserId { get; set; }

        public virtual Topic Topic { get; set; }
        public virtual User User { get; set; }
    }
}
