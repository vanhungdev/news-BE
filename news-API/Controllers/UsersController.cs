using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using news.Api.Services;
using news.Infrastructure.Models;
using news_API.Entities;
using news_API.Infrastructure.Auth;
using news_API.models;

namespace news_API.Controllers
{

 
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpPost("authenticate")]
        public IActionResult Authenticate(AuthRequest model)
        {
            if (model.Username == null || model.Password ==null || model.Username.Equals("") || model.Password.Equals(""))
            {
                return Ok(ResultObject.Fail("Tên đăng nhâp hoặc mật khẩu trống."));
            }
            var response = _userService.Authenticate(model);
            
            if (response == null)
            {
                return Ok(ResultObject.Fail( "Tên đăng nhập hoặc mật khẩu không dúng."));
            }
            response.user.password = null;
            var result = ResultObject.Ok<authReponse>(response,"Đăng nhập thành công");
            return Ok(result);
        }
        [HttpGet("getAllUser")]
        //[Authorize(Roles ="admin")]
        public IEnumerable<User> getAllUser()
        {
            IEnumerable<User> allUser = _userService.GetAll();
            return allUser;
        }
        
    }
}
