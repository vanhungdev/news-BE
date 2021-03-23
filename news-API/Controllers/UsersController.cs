using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using news.Infrastructure.Consts;
using news.Infrastructure.Models;
using news_API.Infrastructure.Auth;
using news_API.models;
using news_API.Services;

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

        [HttpGet("logout")]
        public IActionResult logout(string Username)
        {
            HttpContext.Request.Headers.TryGetValue("HEADER_AUTHORIZATION", out var mbsExternal);
            var userName = User.FindFirstValue(Consts.CLAIM_USERNAME);
            string token = mbsExternal;
            JwtAuthManager.AddTokenToBlacklist(userName, token, token);
            return  Ok(ResultObject.Ok<NullDataType>(null, "Đăng xuất thành công."));
        }
    }
}
