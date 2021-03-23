using System;
using System.Collections.Generic;
using System.Text;

namespace news.Application.Entities
{
    public class Post
    {
        public int ID { get; set; }
        public int Topid { get; set; }
        public string Title { get; set; }
        public string Slug { get; set; }
        public string Detail { get; set; }
        public string Img { get; set; }
        public string Type { get; set; }
        public string Metakey { get; set; }
        public string Metadesc { get; set; }
        public DateTime Created_at { get; set; }
        public int Created_by { get; set; }
        public DateTime Updated_at { get; set; }
        public int Updated_by { get; set; }
        public int Status { get; set; }
    }
}
