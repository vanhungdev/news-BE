using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using news.Api.Services;
using news.Infrastructure.Models;
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
        [Authorize(Roles = "8888,20,9999")]
        [HttpGet]
        public IEnumerable<User> getAllUser()
        {
            IEnumerable<User> allUser = _userService.GetAll();
            return allUser;
        }
       
        [Authorize(Roles = "8888,20,9999")]
        [HttpGet]
        public User findUserById(int id)
        {
            User post = _userService.GetById(id);
            return post;
        }
        [Authorize(Roles = "8888,20,9999")]
        [HttpGet]
        public IEnumerable<Role> getAllRole()
        {
           var list  = _userService.GetAllRole();
            return list;
        }
        
        // only 9999
        [Authorize(Roles = "9999")]
        [HttpGet]
        public ActionResult ChangeStatus(int Id, int Status)
        {
            int result = _userService.changeStatusPost(Id, Status == 1 ? 2 : 1);
            if (result == 1)
            {
                return Ok(ResultObject.Ok<NullDataType>(null, "Cập thật thành công."));
            }
            return Ok(ResultObject.Fail("Thất bại."));
        }
        [Authorize(Roles = "9999")]
        [HttpGet]
        public ActionResult Delete(int Id)
        {
            int result = _userService.delete(Id);
            if (result == 1)
            {
                return Ok(ResultObject.Ok<NullDataType>(null, "Xóa thành công."));
            }
            return Ok(ResultObject.Fail("Thất bại."));
        }
        [Authorize(Roles = "9999")]
        [HttpGet]
        public ActionResult deTrash(int Id)
        {
            int result = _userService.deTrash(Id);
            if (result == 1)
            {
                return Ok(ResultObject.Ok<NullDataType>(null, "Xóa thành công."));
            }
            return Ok(ResultObject.Fail("Thất bại."));
        }
        [Authorize(Roles = "9999")]
        [HttpGet]
        public ActionResult reTrash(int Id)
        {
            int result = _userService.reTrash(Id);
            if (result == 1)
            {
                return Ok(ResultObject.Ok<NullDataType>(null, "Khôi phục thành công."));
            }
            return Ok(ResultObject.Fail("Thất bại."));
        }

        [Authorize(Roles = "8888,20,9999")]
        [HttpGet]
        public IEnumerable<User> getAllTopicTrash()
        {
            var list = _userService.getAllPostTrash();
            return list.ToList();
        }
        [Authorize(Roles = "9999")]
        [HttpPost]
        public ActionResult editUser([FromBody] User user)
        {
            var status = _userService.Edit(user);
            if (status == 1)
            {
                return Ok(ResultObject.Ok<NullDataType>(null, "Cập thật thành công."));
            }
            return Ok(ResultObject.Fail("Thất bại."));
        }
    }
}
