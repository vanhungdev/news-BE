using news.Infrastructure.Configuration;
using news.Infrastructure.Utilities;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace news.Infrastructure.Database
{
    public class RedisDb
    {
        private static AppSettings _appSettings => AppSettingServices.Get;
        private static string _serverRead = _appSettings.RedisSettings.ServerRead;
        private static int _databaseNumber = _appSettings.RedisSettings.DatabaseNumber;
        /// <summary>
        /// SortedSetRemoveRangeByScore
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static void saveTokenToBlackRedis(string key, string value)
        {
            using (var redis = ConnectionMultiplexer.Connect(_serverRead))
            {
                IDatabase database = redis.GetDatabase(_databaseNumber);
                database.ListRightPush(key, Encoding.UTF8.GetBytes(value));
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
            using (var redis = ConnectionMultiplexer.Connect(_serverRead))
            {
                IDatabase database = redis.GetDatabase(_databaseNumber);
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
            using (var redis = ConnectionMultiplexer.Connect(_serverRead))
            {
                // error is here
                IDatabase database = redis.GetDatabase(_databaseNumber);
                result = database.ListRange(key,0,-1).Select(x => x.ToString()).ToList();
            }
            return result;
        }
    }
}
