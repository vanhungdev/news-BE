using news.Application.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace news_API.models
{
      public class authReponse
    {
        public string token { get; set; }
        public User user { get; set; }
        public authReponse(string _token, User _user )
        {
            token = _token;
            user = _user;
        }
    }
}
