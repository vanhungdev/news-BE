using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using news.Application.User.CommandHandler;
using news.Application.User.Queries;
using news.Application.User.QueryHandler;
using news.Infrastructure.Models;

namespace news_API.Areas.Controllers
{
    [Area("Admin")]
    public class UsersController : Controller
    {
        private readonly IMediator _mediator;
        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [Authorize(Roles = "8888,20,9999")]
        [HttpGet]
        public async Task<ActionResult> getAllUser()
        {
            var result = _mediator.Send(new GetAllUserRequest());
            return Ok(await result);
        }
       
        [Authorize(Roles = "8888,20,9999")]
        [HttpGet]
        public async Task<ActionResult> findUserById(int id)
        {
            var result = _mediator.Send(new GetUserByIdRequest { Id = id });
            return Ok(await result);
        }
        [Authorize(Roles = "8888,20,9999")]
        [HttpGet]
        public async Task<ActionResult> getAllRole()
        {
            var result = _mediator.Send(new GetAllRoleRequest());
            return Ok(await result);
        }
        
        // only 9999
        [Authorize(Roles = "9999")]
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
            int result = await _mediator.Send(new ChangeStatusUserRequest { Id = Id, Status = Status });
            if (result == 1)
            {
                return Ok(ResultObject.Ok<NullDataType>(null, "Cập thật thành công."));
            }
            return Ok(ResultObject.Fail("Thất bại."));
        }
        [Authorize(Roles = "9999")]
        [HttpGet]
        public async Task<ActionResult> Delete(int Id)
        {
            var result = await _mediator.Send(new DeleteUserRequest { Id = Id });
            if (result == 1)
            {
                return Ok(ResultObject.Ok<NullDataType>(null, "Xóa thành công."));
            }
            return Ok(ResultObject.Fail("Thất bại."));
        }
        [Authorize(Roles = "9999")]
        [HttpGet]
        public async Task<ActionResult> deTrash(int Id)
        {
            var result = await _mediator.Send(new DeTrashUserRequest { Id = Id });
            if (result == 1)
            {
                return Ok(ResultObject.Ok<NullDataType>(null, "Xóa thành công."));
            }
            return Ok(ResultObject.Fail("Thất bại."));
        }
        [Authorize(Roles = "9999")]
        [HttpGet]
        public async Task<ActionResult> reTrash(int Id)
        {
            var result = await _mediator.Send(new ReTrashUserRequest { Id = Id });
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
            var result = await _mediator.Send(new GetAllUserTrashRequest());
            return Ok(result);
        }
        [Authorize(Roles = "9999")]
        [HttpPost]
        public async Task<ActionResult> editUser([FromBody] EditUserRequest user)
        {
            var result = await _mediator.Send(user);
            if (result == 1)
            {
                return Ok(ResultObject.Ok<NullDataType>(null, "Cập thật thành công."));
            }
            return Ok(ResultObject.Fail("Thất bại."));
        }
    }
}
