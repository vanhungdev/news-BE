using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace news.Infrastructure.Database
{
    public class RedisDb
    {
        /// <summary>
        /// SortedSetRemoveRangeByScore
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static void saveTokenToBlackRedis(string key, string value)
        {
            using (var redis = ConnectionMultiplexer.Connect("127.0.0.1:6379"))
            {
                IDatabase database = redis.GetDatabase(0);
                //database.SetAdd(key, Encoding.UTF8.GetBytes(value));
            }
        }

        /// <summary>
        /// SortedSetRemoveRangeByScore
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="score"></param>
        /// <returns></returns>
        public static void SortedSetRemoveRangeByScore(string key, double score)
        {
            using (var redis = ConnectionMultiplexer.Connect("127.0.0.1:6379"))
            {
                IDatabase database = redis.GetDatabase();
                database.SortedSetRemoveRangeByScore(key, 0, score);
            }
        }

        /// <summary>
        /// SortedSetRange
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static List<string> SortedSetRange(string key)
        {
            var result = new List<string>();
            using (var redis = ConnectionMultiplexer.Connect("127.0.0.1:6379"))
            {
                // error is here
                IDatabase database = redis.GetDatabase(0);
                var aresult = database.SortedSetRangeByScore(key);
            }
            return result;
        }
    }
}
