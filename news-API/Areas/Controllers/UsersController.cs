using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using news.Api.Services;
using news_API.Entities;

namespace news_API.Areas.Controllers
{
    [Area("Admin")]
    public class UsersController : Controller
    {
        private IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }
        [Authorize(Roles = "8888,20,999")]
        [HttpGet]
        public IEnumerable<User> getAllUser()
        {
            IEnumerable<User> allUser = _userService.GetAll();
            return allUser;
        }

        // only 9999
    }
}
