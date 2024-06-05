using HocWeb.Infrastructure.Entities;
using HocWeb.Service.Interfaces;
using HocWeb.Service.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HocWeb.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet("get-all")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _categoryService.GetAll();
            return Ok(new ApiResult<IList<Category>>()
            {
                Data = result,
                StatusCode = 200,
            });
        }

        [HttpGet("get-by-id")]
        public async Task<IActionResult> GetById([FromQuery] int id)
        {
            try
            {
                var result = await _categoryService.GetById(id);
                if (result != null)
                    return Ok(new ApiResult<Category>()
                    {
                        Data = result,
                        StatusCode = 200,
                    });
                return BadRequest(new ApiResult<Category>()
                {
                    Data = null,
                    StatusCode = 500,
                });
            }
            catch (Exception e)
            {
                return BadRequest(new ApiResult<Category>()
                {
                    Data = null,
                    Message = e.Message,
                    StatusCode = 500,
                });
            }
        }

        [Authorize]
        [HttpPost("add")]
        public async Task<IActionResult> Add([FromBody] Category model)
        {
            try
            {
                var result = await _categoryService.Add(model);
                if (result != null)
                {
                    return Ok(new ApiResult<Category>()
                    {
                        Data = result,//
                        StatusCode = 200,
                    });
                }
                return BadRequest(new ApiResult<Category>()
                {
                    Data = null,
                    Message = "Không thể thêm mới ",
                    StatusCode = 500,
                });
            }
            catch (Exception e)
            {
                return BadRequest(new ApiResult<Category>()
                {
                    Data = null,
                    Message = e.Message,
                    StatusCode = 500,
                });
            }
        }

    }
}
