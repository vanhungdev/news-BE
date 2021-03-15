using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        [HttpPost("Edit")]
        public ActionResult edit([FromBody]  Category topic)
        {
            int editStatus = _categoryService.edit(topic);
            if (editStatus == 1)
            {
                return Ok(ResultObject.Ok<NullDataType>(null, "Cập nhật thành công."));
            }
            return Ok(ResultObject.Fail("Thất bại."));
        }
        [HttpPost("Create")]
        public ActionResult Create([FromBody] Category topic)
        {
            int editStatus = _categoryService.create(topic);
            if (editStatus == 1)
            {
                return Ok(ResultObject.Ok<NullDataType>(null, "Thêm thành công."));
            }
            return Ok(ResultObject.Fail("Thất bại."));
        }
        // 
        [HttpGet("ChangeStatus")]
        public ActionResult ChangeStatus(int Id,int Status)
        {
          
            int result = _categoryService.changeStatusTopic(Id, Status == 1 ? 2 : 1);
            if (result == 1)
            {
                return Ok(ResultObject.Ok<NullDataType>(null, "Thay đổi trạng thái thành công."));
            }
            return Ok(ResultObject.Fail("Thất bại."));
        }

        [HttpGet("Delete")]
        public ActionResult Delete(int Id)
        {
            int result = _categoryService.delete(Id);
            if (result == 1)
            {
                return Ok(ResultObject.Ok<NullDataType>(null, "Xóa Vĩnh viễn thành công."));
            }
            return Ok(ResultObject.Fail("Thất bại."));
        }
        [HttpGet("DeTrash")]
        public ActionResult deTrash(int Id)
        {
            int result = _categoryService.deTrash(Id);
            if (result == 1)
            {
                return Ok(ResultObject.Ok<NullDataType>(null, "Xóa thành công."));
            }
            return Ok(ResultObject.Fail("Thất bại."));
        }

        [HttpGet("ReTrash")]
        public ActionResult reTrash(int Id)
        {
            int result = _categoryService.reTrash(Id);
            if (result == 1)
            {
                return Ok(ResultObject.Ok<NullDataType>(null, "Khôi phục thành công."));
            }
            return Ok(ResultObject.Fail("Thất bại."));
        }

        [HttpGet("getAllTopicTrash")]
        public IEnumerable<Category> getAllTopicTrash()
        {
            var list = _categoryService.getAllTopicTrash();
            return list;
        }

    }
}
