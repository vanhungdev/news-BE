using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using news.Application.Category.Commands;
using news.Application.Category.Queries;
using news.Infrastructure.Models;

namespace news_API.Areas.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly IMediator _mediator;
        public CategoryController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [Authorize(Roles = "8888,20,9999")]
        [HttpGet]
        public async Task<ActionResult> getAll()
        {
            var result = _mediator.Send(new GetAllCategory());
            return Ok(await result);
        }
        [Authorize(Roles = "8888,20,9999")]
        [HttpGet]
        public async Task<ActionResult> findById(int id)
        {
            var result = _mediator.Send(new GetCategoryById { Id = id });
            return Ok(await result);
        }
        [Authorize(Roles = "20,9999")]
        [HttpPost]
        public async Task<ActionResult> edit([FromBody] EditCategotyRequest topic)
        {
            int editStatus = await _mediator.Send(topic);
            if (editStatus == 1)
            {
                return Ok(ResultObject.Ok<NullDataType>(null, "Cập nhật thành công."));
            }
            return Ok(ResultObject.Fail("Thất bại."));
        }
        [Authorize(Roles = "20,9999")]
        [HttpPost]
        public async Task<ActionResult> Create([FromBody] CreateCategoryRequest topic)
        {
            int editStatus = await _mediator.Send(topic);
            if (editStatus == 1)
            {
                return Ok(ResultObject.Ok<NullDataType>(null, "Thêm thành công."));
            }
            return Ok(ResultObject.Fail("Thất bại."));
        }
        [Authorize(Roles = "20,9999")]
        // 
        [HttpGet]
        public async Task<ActionResult> ChangeStatus(int Id, int Status)
        {
            if(Status == 2)
            {
                Status = 1;
            }
            else
            {
                Status = 2;
            }
            int result = await _mediator.Send(new ChangeStatusCategoryRequest { Id = Id, Status = Status });

            if (result == 1)
            {
                return Ok(ResultObject.Ok<NullDataType>(null, "Thay đổi trạng thái thành công."));
            }
            return Ok(ResultObject.Fail("Thất bại."));
        }
        [Authorize(Roles = "20,9999")]
        [HttpGet]
        public async Task<ActionResult> Delete(int Id)
        {
            int result = await _mediator.Send(new DeleteCategoryRequest { Id = Id});
            if (result == 1)
            {
                return Ok(ResultObject.Ok<NullDataType>(null, "Xóa Vĩnh viễn thành công."));
            }
            return Ok(ResultObject.Fail("Thất bại."));
        }
        [Authorize(Roles = "20,9999")]
        [HttpGet]
        public async Task<ActionResult> deTrash(int Id)
        {
            int result = await _mediator.Send(new DeTrashCategoryRequest { Id = Id });
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
            int result =await _mediator.Send(new ReTrashCategoryRequest { Id = Id });
            if (result == 1)
            {
                return Ok(ResultObject.Ok<NullDataType>(null, "Khôi phục thành công."));
            }
            return Ok(ResultObject.Fail("Thất bại."));
        }
        [Authorize(Roles = "8888,20,9999")]

        [HttpGet]
        public async Task<ActionResult> getAllTopicTrash()
        {
            var list = await _mediator.Send(new GetAllTopicTrashCategoryRequest());
            return Ok(list);
        }
    }
}
