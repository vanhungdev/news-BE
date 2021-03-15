using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        public CUDResult edit([FromBody]  Category topic)
        {
            int editStatus = _categoryService.edit(topic);
            if(editStatus ==1)
            {
                return new CUDResult(1, "Cập nhật thành công");
            }
            return new CUDResult(0, "Cập nhật thất bại");
        }
        [HttpPost("Create")]
        public CUDResult Create([FromBody] Category topic)
        {
            int editStatus = _categoryService.create(topic);
            if (editStatus == 1)
            {
                return new CUDResult(1, "Cập nhật thành công");
            }
            return new CUDResult(0, "Cập nhật thất bại");
        }
        // 
        [HttpGet("ChangeStatus")]
        public CUDResult ChangeStatus(int Id,int Status)
        {
            if(Status <1 || Status>2)
            {
                return new CUDResult(0, "Thất bại,Trạng thái không hợp lệ.");
            }    
            int result = _categoryService.changeStatusTopic(Id, Status == 1 ? 2 : 1);
            if (result == 1)
            {
                return new CUDResult(1, "Thay đổi trạng thái thành công.");
            }
            return new CUDResult(0, "Thay đổi trạng thái thất bại.");
        }

        [HttpGet("Delete")]
        public CUDResult Delete(int Id)
        {
            int result = _categoryService.delete(Id);
            if (result == 1)
            {
                return new CUDResult(1, "Xóa vĩnh viễn thành công");
            }
            return new CUDResult(0, "Xóa thất bại");
        }
        [HttpGet("DeTrash")]
        public CUDResult deTrash(int Id)
        {
            int result = _categoryService.deTrash(Id);
            if (result == 1)
            {
                return new CUDResult(1, "Xóa thành công");
            }
            return new CUDResult(0, "Xóa thất bại");
        }

        [HttpGet("ReTrash")]
        public CUDResult reTrash(int Id)
        {
            int result = _categoryService.reTrash(Id);
            if (result == 1)
            {
                return new CUDResult(1, "khôi phục thành công");
            }
            return new CUDResult(0, "Khôi phục thất bại");
        }

        [HttpGet("getAllTopicTrash")]
        public IEnumerable<Category> getAllTopicTrash()
        {
            var list = _categoryService.getAllTopicTrash();
            return list;
        }

    }
}
