using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using news.Infrastructure.Models;
using news_API.Entities;
using news_API.Services;

namespace news_API.Areas.Controllers
{
    [Area("Admin")]
    public class PostsController : Controller
    {
        private readonly IPostService _postService;

        public PostsController(IPostService postService)
        {
            _postService = postService;
        }
        [Authorize(Roles = "8888,20,9999")]
        [HttpGet]
        public IEnumerable<Post> getAllPost()
        {
            IEnumerable<Post> allPost = _postService.getAllPost();
            return allPost;
        }

        [Authorize(Roles = "20,9999")]
        [HttpPost]
        public ActionResult editPost([FromBody] Post post)
        {

            var status = _postService.editPost(post);
            if (status == 1)
            {
                return Ok(ResultObject.Ok<NullDataType>(null, "Cập thật thành công."));
            }
            return Ok(ResultObject.Fail("Thất bại."));
        }

        [Authorize(Roles = "20,9999")]
        [HttpPost]
        public ActionResult createPost([FromBody] Post post)
        {
            var status = _postService.createPost(post);
            if (status == 1)
            {
                return Ok(ResultObject.Ok<NullDataType>(null, "Thêm thật thành công."));
            }
            return Ok(ResultObject.Fail("Thất bại."));
        }

        [Authorize(Roles = "20,9999")]
        [HttpGet]
        public ActionResult ChangeStatus(int Id, int Status)
        {

            int result = _postService.changeStatusPost(Id, Status == 1 ? 2 : 1);
            if (result == 1)
            {
                return Ok(ResultObject.Ok<NullDataType>(null, "Cập thật thành công."));
            }
            return Ok(ResultObject.Fail("Thất bại."));
        }
        [Authorize(Roles = "20,9999")]
        [HttpGet]
        public ActionResult Delete(int Id)
        {
            int result = _postService.delete(Id);
            if (result == 1)
            {
                return Ok(ResultObject.Ok<NullDataType>(null, "Xóa thành công."));
            }
            return Ok(ResultObject.Fail("Thất bại."));
        }
        [Authorize(Roles = "20,9999")]
        [HttpGet]
        public ActionResult deTrash(int Id)
        {
            int result = _postService.deTrash(Id);
            if (result == 1)
            {
                return Ok(ResultObject.Ok<NullDataType>(null, "Xóa thành công."));
            }
            return Ok(ResultObject.Fail("Thất bại."));
        }
        [Authorize(Roles = "20,9999")]
        [HttpGet]
        public ActionResult reTrash(int Id)
        {
            int result = _postService.reTrash(Id);
            if (result == 1)
            {
                return Ok(ResultObject.Ok<NullDataType>(null, "Xóa thành công."));
            }
            return Ok(ResultObject.Fail("Thất bại."));
        }

        [Authorize(Roles = "8888,20,9999")]
        [HttpGet]
        public IEnumerable<Post> getAllTopicTrash()
        {
            var list = _postService.getAllPostTrash();
            return list;
        }
    }
}
