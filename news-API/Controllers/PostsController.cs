using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using news.Application.Post.Queries;
using news.Infrastructure.Enums;
using news.Infrastructure.Models;
using news_API.models;

namespace news_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class PostsController : ControllerBase
    {
        private readonly IMediator _mediator;
        public PostsController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet("getAllPost")]
        public async Task<ActionResult> getAllPost()
        {
            var allPost = await _mediator.Send(new GetAllPostRequest());
            return Ok( allPost);
        }
        [HttpGet("findPostById/{id}")]
        public async Task<ActionResult> findPostById(int id)
        {
            var post = await _mediator.Send(new GetPostByIdRequest { Id = id});
            return Ok(post);
        }
        [HttpGet("findPostBySlug/{Slug}")]
        public async Task<ActionResult> findPostBySlug(string slug)
        {
            
            var post = await _mediator.Send(new GetPostBySlug { slug = slug });
            return Ok(post);
        }
        [HttpGet("getallPostByCategoryId/{id}")]
        public async Task<ActionResult> getallPostByCategoryId(int id)
        {
            var List = await _mediator.Send(new GetAllPostByCategoryId { Catid  = id});
            return Ok(List);
        }

    }
}
