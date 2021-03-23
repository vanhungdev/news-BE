using System;
using System.Collections.Generic;
using System.Text;

namespace news.Application.Entities
{
    public class Role
    {
        public int Id { get; set; }
        public int Access { get; set; }
        public string description { get; set; }
        public string name { get; set; }
    }
}
