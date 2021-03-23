using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using news.Application.Entities;
using news.Application.Post.CommandHandler;
using news.Application.Post.Queries;
using news.Infrastructure.Models;
namespace news_API.Areas.Controllers
{
    [Area("Admin")]
    public class PostsController : Controller
    {
        private readonly IMediator _mediator;
        public PostsController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [Authorize(Roles = "8888,20,9999")]
        [HttpGet]
        public async Task<ActionResult> getAllPost()
        {
            var allPost = await _mediator.Send(new GetAllPostRequest());
            return Ok(allPost);
        }

        [Authorize(Roles = "20,9999")]
        [HttpPost]
        public async Task<ActionResult> editPost([FromBody] EditPostRequest post)
        {

            var status = await _mediator.Send(post);
            if (status == 1)
            {
                return Ok(ResultObject.Ok<NullDataType>(null, "Cập thật thành công."));
            }
            return Ok(ResultObject.Fail("Thất bại."));
        }

        [Authorize(Roles = "20,9999")]
        [HttpPost]
        public async Task<ActionResult> createPost([FromBody] CreatePostRequest post)
        {
            var status = await _mediator.Send(post);
            if (status == 1)
            {
                return Ok(ResultObject.Ok<NullDataType>(null, "Thêm thật thành công."));
            }
            return Ok(ResultObject.Fail("Thất bại."));
        }

        [Authorize(Roles = "20,9999")]
        [HttpGet]
        public async Task<ActionResult> ChangeStatus(int Id, int Status)
        {
            if (Status == 2)
            {
                Status = 1;
            }
            else
            {
                Status = 2;
            }
            int result = await _mediator.Send(new ChangeStatusPostRequest { Id = Id, Status = Status });
            if (result == 1)
            {
                return Ok(ResultObject.Ok<NullDataType>(null, "Cập thật thành công."));
            }
            return Ok(ResultObject.Fail("Thất bại."));
        }
        [Authorize(Roles = "20,9999")]
        [HttpGet]
        public async Task<ActionResult> Delete(int Id)
        {
            int result = await _mediator.Send(new DeletePostRequest { Id = Id });
            if (result == 1)
            {
                return Ok(ResultObject.Ok<NullDataType>(null, "Xóa thành công."));
            }
            return Ok(ResultObject.Fail("Thất bại."));
        }
        [Authorize(Roles = "20,9999")]
        [HttpGet]
        public async Task<ActionResult> deTrash(int Id)
        {
            int result = await _mediator.Send(new DeTrashPostRequest { Id = Id });
            if (result == 1)
            {
                return Ok(ResultObject.Ok<NullDataType>(null, "Xóa thành công."));
            }
            return Ok(ResultObject.Fail("Thất bại."));
        }
        [Authorize(Roles = "20,9999")]
        [HttpGet]
        public async Task<ActionResult> reTrash(int Id)
        {
            int result = await _mediator.Send(new ReTrashPostRequest { Id = Id });
            if (result == 1)
            {
                return Ok(ResultObject.Ok<NullDataType>(null, "Xóa thành công."));
            }
            return Ok(ResultObject.Fail("Thất bại."));
        }
        [Authorize(Roles = "8888,20,9999")]
        [HttpGet]
        public async Task<ActionResult> getAllPostTrash()
        {
            var allPost = await _mediator.Send(new GetAllPostTrashRequest());
            return Ok(allPost);
        }
    }
}
