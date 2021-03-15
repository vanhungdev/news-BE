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
    [AllowAnonymous]
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
    }
}
