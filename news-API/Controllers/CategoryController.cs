using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using news.Infrastructure.Models;
using news_API.models;
using news.Application.Category.Commands;
using news.Application.Category.Queries;

namespace news_API.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class CategoryController : ControllerBase
    {     
        private readonly IMediator _mediator;
        public CategoryController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet("getAll")]
        public async Task<ActionResult>  getAll()
        {
            var result = _mediator.Send(new GetAllCategory());
            return Ok(await result);
        }
        [HttpGet("findById/{id}")]
        public async Task<ActionResult> findById(int id)
        {
            var result = _mediator.Send(new GetCategoryById { Id =id});
            return Ok(await result);
        }
        [HttpGet("findBySlug/{Slug}")]
        public async Task<ActionResult> findBySlug(string slug)
        {
            var result = _mediator.Send(new GetCategoryBySlug { slug = slug });
            return Ok(await result);
        }
    }
}
