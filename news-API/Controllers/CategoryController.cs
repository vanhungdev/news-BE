using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using news.Infrastructure.Models;
using news_API.Entities;
using news_API.models;
using news_API.Services;

namespace news_API.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class CategoryController : ControllerBase
    {
        private ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        [HttpGet("getAll")]
        public IEnumerable<Category> getAll()
        {
            var list = _categoryService.getAll();
            return list;
        }
        [HttpGet("findById/{id}")]
        public Category findById(int id)
        {
            Category list = _categoryService.findById(id);
            return list;
        }
    }
}
