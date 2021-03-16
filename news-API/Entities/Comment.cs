using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace news_API.Entities
{
    public class Comment
    {
        public int Id{get;set;}
        public int PostId { get; set; }
        public int ParentId { get; set; }
        public string CommentDetail { get; set; }
        public string Name { get; set; }
        public int Star { get; set; }
        public DateTime Create_at { get; set; }
        public int Status { get; set; }

    }
}
