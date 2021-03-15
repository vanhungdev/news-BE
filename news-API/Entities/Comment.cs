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
        public string CommentDetail { get; set; }
        public int star { get; set; }
        public DateTime Create_at { get; set; }
        public int Create_by { get; set; }

    }
}
