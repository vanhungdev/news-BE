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
        [Authorize(Roles = "20,999")]
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

        //[Authorize(Roles = "29, 999")]
        [HttpPost("EditPost")]
        public CUDResult editPost(Post post)
        {

            var status = _postService.editPost(post);
            if (status == 1)
            {
                return new CUDResult(1, "Cập nhật thành công");
            }
            return new CUDResult(0, "Cập nhật thất bại");
        }

        //[Authorize(Roles = "20,999")]
        [HttpPost("createPost")]
        public CUDResult createPost(Post post)
        {
            var status = _postService.createPost(post);
            if (status == 1)
            {
                return new CUDResult(1, "Cập nhật thành công");
            }
            return new CUDResult(0, "Cập nhật thất bại");
        }
        // 
        [HttpGet("ChangeStatus")]
        public CUDResult ChangeStatus(int Id, int Status)
        {
            if (Status < 1 || Status > 2)
            {
                return new CUDResult(0, "Thất bại,Trạng thái không hợp lệ.");
            }
            int result = _postService.changeStatusPost(Id, Status == 1 ? 2 : 1);
            if (result == 1)
            {
                return new CUDResult(1, "Thay đổi trạng thái thành công.");
            }
            return new CUDResult(0, "Thay đổi trạng thái thất bại.");
        }

        [HttpGet("Delete")]
        public CUDResult Delete(int Id)
        {
            int result = _postService.delete(Id);
            if (result == 1)
            {
                return new CUDResult(1, "Xóa vĩnh viễn thành công");
            }
            return new CUDResult(0, "Xóa thất bại");
        }
        [HttpGet("DeTrash")]
        public CUDResult deTrash(int Id)
        {
            int result = _postService.deTrash(Id);
            if (result == 1)
            {
                return new CUDResult(1, "Xóa thành công");
            }
            return new CUDResult(0, "Xóa thất bại");
        }

        [HttpGet("ReTrash")]
        public CUDResult reTrash(int Id)
        {
            int result = _postService.reTrash(Id);
            if (result == 1)
            {
                return new CUDResult(1, "khôi phục thành công");
            }
            return new CUDResult(0, "Khôi phục thất bại");
        }

        [HttpGet("getAllTopicTrash")]
        public IEnumerable<Post> getAllTopicTrash()
        {
            var list = _postService.getAllPostTrash();
            return list;
        }


    }
}
