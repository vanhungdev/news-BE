using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using news.Infrastructure.Models;
using news_API.Services;

namespace news_API.Areas.Controllers
{
    [Area("Admin")]
    public class CommentController : Controller
    {
        private readonly ICommentService _commentService;

        public CommentController(ICommentService commentService)
        {
            _commentService = commentService;
        }

        [Authorize(Roles = "20,9999")]
        [HttpGet]
        public ActionResult ChangeStatusComment(int Id, int Status)
        {
            int result = _commentService.changeStatusComment(Id, Status == 1 ? 2 : 1);
            if (result == 1)
            {
                return Ok(ResultObject.Ok<NullDataType>(null, "Thay đổi trạng thái thành công."));
            }
            return Ok(ResultObject.Fail("Thất bại."));
        }
    }
}
