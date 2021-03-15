using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace news_API.Entities
{
    public partial class Role
    {
        public int Id { get; set; }
        public int Access { get; set; }
        public string description { get; set; }
        public string name { get; set; }
    }
}
