using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using news.Application.Comment.Commands;
using news.Infrastructure.Models;

namespace news_API.Areas.Controllers
{
    [Area("Admin")]
    public class CommentController : Controller
    {
        private readonly IMediator _mediator;
        public CommentController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Authorize(Roles = "20,9999")]
        [HttpGet]
        public async Task<ActionResult> ChangeStatusComment(int Id, int Status)
        {
            if (Status == 2)
            {
                Status = 1;
            }
            else
            {
                Status = 2;
            }
            int result = await _mediator.Send( new ChangeStatusCommentRequest { Id = Id, Status = Status });
            if (result == 1)
            {
                return Ok(ResultObject.Ok<NullDataType>(null, "Thay đổi trạng thái thành công."));
            }
            return Ok(ResultObject.Fail("Thất bại."));
        }
    }
}
