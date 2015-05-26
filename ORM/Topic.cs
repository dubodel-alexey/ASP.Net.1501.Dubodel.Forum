﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORM
{
    public class Topic
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool ForRegistered { get; set; }

        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }
    }
}
