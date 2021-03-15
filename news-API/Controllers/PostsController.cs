using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using news.Infrastructure.Enums;
using news.Infrastructure.Models;
using news_API.Entities;
using news_API.models;
using news_API.Services;

namespace news_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly IPostService _postService;

        public PostsController(IPostService postService)
        {
            _postService = postService;
        }
        [HttpGet("getAllPost")]
        public IEnumerable<Post> getAllPost()
        {
            IEnumerable<Post> allPost = _postService.getAllPost();
            return allPost;
        }
        [HttpGet("findPostById/{id}")]
        public Post findPostById(int id)
        {
            Post post = _postService.findPostById(id).FirstOrDefault();
            return post;
        }
        [HttpGet("findPostBySlug/{Slug}")]
        public IEnumerable<Post> findPostBySlug(string slug)
        {
            var allPost = _postService.findPostBySlug(slug);
            return allPost;
        }

        [HttpGet("getPostByCategoryId/{id}")]
        public IEnumerable<Post> findPostBySlug(int id)
        {
            var allPost = _postService.getPostByCategoryId(id);
            return allPost;
        }

        //[Authorize(Roles = "20,999")]
        [HttpPost("EditPost")]
        public ActionResult editPost(Post post)
        {

            var status = _postService.editPost(post);
            if (status == 1)
            {
                return Ok(ResultObject.Ok<NullDataType>(null,"Cập thật thành công."));
            }
            return Ok(ResultObject.Fail("Thất bại."));
        }

        [Authorize(Roles = "20,999")]
        [HttpPost("createPost")]
        public ActionResult createPost(Post post)
        {
            var status = _postService.createPost(post);
            if (status == 1)
            {
                return Ok(ResultObject.Ok<NullDataType>(null, "Thêm thật thành công."));
            }
            return Ok(ResultObject.Fail("Thất bại."));
        }

        [Authorize(Roles = "20,999")]
        [HttpGet("ChangeStatus")]
        public ActionResult ChangeStatus(int Id, int Status)
        {
          
            int result = _postService.changeStatusPost(Id, Status == 1 ? 2 : 1);
            if (result == 1)
            {
                return Ok(ResultObject.Ok<NullDataType>(null, "Cập thật thành công."));
            }
            return Ok(ResultObject.Fail("Thất bại."));
        }
        [Authorize(Roles = "20,999")]
        [HttpGet("Delete")]
        public ActionResult Delete(int Id)
        {
            int result = _postService.delete(Id);
            if (result == 1)
            {
                return Ok(ResultObject.Ok<NullDataType>(null, "Xóa thành công."));
            }
            return Ok(ResultObject.Fail("Thất bại."));
        }
        [Authorize(Roles = "20,999")]
        [HttpGet("DeTrash")]
        public ActionResult deTrash(int Id)
        {
            int result = _postService.deTrash(Id);
            if (result == 1)
            {
                return Ok(ResultObject.Ok<NullDataType>(null, "Xóa thành công."));
            }
            return Ok(ResultObject.Fail("Thất bại."));
        }
        [Authorize(Roles = "20,999")]
        [HttpGet("ReTrash")]
        public ActionResult reTrash(int Id)
        {
            int result = _postService.reTrash(Id);
            if (result == 1)
            {
                return Ok(ResultObject.Ok<NullDataType>(null, "Xóa thành công."));
            }
            return Ok(ResultObject.Fail("Thất bại."));
        }

        [HttpGet("getAllTopicTrash")]
        public IEnumerable<Post> getAllTopicTrash()
        {
            var list = _postService.getAllPostTrash();
            return list;
        }
    }
}
