using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using news.Application.Comment.Commands;
using news.Application.Comment.Queries;
using news.Infrastructure.Models;

namespace news_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class CommentController : Controller
    {
        private readonly IMediator _mediator;
        public CommentController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet("GetAllCommentByPost/{Id}")]
        public async Task<ActionResult> GetAllCommentByPost(int Id)
        {
            var result = await _mediator.Send(new GetAllCommentByPostId { postId = Id});          
            return Ok (result);
        }

        [HttpPost("createComment")]
        public async Task<ActionResult> createComment([FromBody] CreateCommentRequest comment)
        {
            var status = await _mediator.Send(comment);
            if (status == 1)
            {
                return Ok(ResultObject.Ok<NullDataType>(null, "Thêm  thành công."));
            }
            return Ok(ResultObject.Fail("Thất bại."));
        }

    }
}
