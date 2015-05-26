using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcPL.ViewModels
{
    public class UserViewModel
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Role { get; set; }
        public DateTime RegisteredTime { get; set; }
       // public string avatarPath { get; set; }
    }
}