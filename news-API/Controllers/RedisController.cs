using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using news.Api.Services;
using news_API.Entities;
using Newtonsoft.Json;

namespace news_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RedisController : Controller
    {
        private readonly IDistributedCache _distributedCache;
        private readonly IUserService _userService;
       
        public RedisController(IDistributedCache distributedCache, IUserService userService)
        {
            _distributedCache = distributedCache;
            _userService = userService;
        }
        [HttpGet("index")]
        public ActionResult index()
        {
            return View();
        }

        [HttpGet("GetAllUserUsingRedisCache")]
        public async Task<IActionResult> GetAllUserUsingRedisCache()
        {
            var cacheKey = "USERLIST";
            string serializedCustomerList;
            var UserList = new List<User>();
            var redisCustomerList = await _distributedCache.GetAsync(cacheKey);
            if (redisCustomerList != null)
            {
                serializedCustomerList = Encoding.UTF8.GetString(redisCustomerList);
                UserList = JsonConvert.DeserializeObject<List<User>>(serializedCustomerList);
            }
            else
            {             
                 UserList =  _userService.GetAll().ToList();
                serializedCustomerList = JsonConvert.SerializeObject(UserList);
                redisCustomerList = Encoding.UTF8.GetBytes(serializedCustomerList);
                var options = new DistributedCacheEntryOptions()
                    .SetAbsoluteExpiration(DateTime.Now.AddMinutes(10))
                    .SetSlidingExpiration(TimeSpan.FromMinutes(2));
                await _distributedCache.SetAsync(cacheKey, redisCustomerList, options);
            }
            return Ok(UserList);
        }
    }
}
