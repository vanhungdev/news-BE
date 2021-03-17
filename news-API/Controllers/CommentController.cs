using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using news.Infrastructure.Models;
using news_API.Entities;
using news_API.Services;

namespace news_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class CommentController : Controller
    {
        private readonly ICommentService _commentService;

        public CommentController(ICommentService commentService)
        {
            _commentService = commentService;
        }
        [HttpGet("GetAllCommentByPost/{Id}")]
        public IEnumerable<Comment> GetAllCommentByPost(int Id)
        {
            var listComment = _commentService.GetAllCommentByPost(Id);
            return listComment;
        }

        [HttpPost("createComment")]
        public ActionResult createComment([FromBody] Comment comment)
        {
            var status = _commentService.create(comment);
            if (status == 1)
            {
                return Ok(ResultObject.Ok<NullDataType>(null, "Thêm  thành công."));
            }
            return Ok(ResultObject.Fail("Thất bại."));
        }

    }
}
