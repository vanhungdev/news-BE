using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace news.Database
{
   public interface  IQuery
    {
        public int Execute(string sql, object param = null);
         IEnumerable<T> Query<T>(int commandType , string sql, object param = null);
    }
}
