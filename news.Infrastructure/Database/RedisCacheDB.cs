using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace news.Infrastructure.Database
{
    public interface IRedisCacheDB
    {
         Task<IEnumerable<T>> setOrGetdatabaseFromRidisDB<T>(string key, IEnumerable<T> List);    
    }
    public class RedisCacheDB: IRedisCacheDB
    {
        private readonly IDistributedCache _distributedCache;
        public RedisCacheDB(IDistributedCache distributedCache)
        {
            _distributedCache = distributedCache;
        }

        public async Task<IEnumerable<T>> setOrGetdatabaseFromRidisDB<T>(string key, IEnumerable<T> List)
        {
            string serializedList;
            var redisList = await _distributedCache.GetAsync(key);
            if (redisList != null)
            {
                serializedList = Encoding.UTF8.GetString(redisList);
                IEnumerable<T> listOfDB = JsonConvert.DeserializeObject<List<T>>(serializedList);
                return listOfDB;
            }
            else
            {
                serializedList = JsonConvert.SerializeObject(List);
                redisList = Encoding.UTF8.GetBytes(serializedList);
                var options = new DistributedCacheEntryOptions()
                    .SetAbsoluteExpiration(DateTime.Now.AddMinutes(10))
                    .SetSlidingExpiration(TimeSpan.FromMinutes(2));
                await _distributedCache.SetAsync(key, redisList, options);
            }
            return List;
        }    
    }
}
