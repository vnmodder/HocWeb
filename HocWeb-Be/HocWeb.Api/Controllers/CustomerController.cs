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
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpGet("get-all")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _customerService.GetAll();
            return Ok(new ApiResult<IList<Customer>>()
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
                var result = await _customerService.GetById(id);
                if (result != null)
                    return Ok(new ApiResult<Customer>()
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
        public async Task<IActionResult> Add([FromBody] Customer model)
        {
            try
            {
                var result = await _customerService.Add(model);
                if (result != null)
                {
                    return Ok(new ApiResult<Customer>()
                    {
                        Data = result,
                        StatusCode = 200,
                    });
                }
                return BadRequest(new ApiResult<Customer>()
                {
                    Data = null,
                    Message = "Không thể thêm mới ",
                    StatusCode = 500,
                });
            }
            catch (Exception e)
            {
                return BadRequest(new ApiResult<Customer>()
                {
                    Data = null,
                    Message = e.Message,
                    StatusCode = 500,
                });
            }
        }

        [Authorize]
        [HttpPost("delete")]
        public async Task<IActionResult> Delete([FromBody] int id)
        {
            try
            {
                var result = await _customerService.Delete(id);
                if (result == true)
                {
                    return Ok(new ApiResult<Customer>()
                    {
                        StatusCode = 200,
                    });
                }
                return BadRequest(new ApiResult<Customer>()
                {
                    Data = null,
                    Message = "Không xóa thêm",
                    StatusCode = 500,
                });
            }
            catch (Exception e)
            {
                return BadRequest(new ApiResult<Customer>()
                {
                    Data = null,
                    Message = e.Message,
                    StatusCode = 500,
                });
            }
        }

        [Authorize]
        [HttpPost("update")]
        public async Task<IActionResult> Update([FromBody] Customer model)
        {
            try
            {
                var result = await _customerService.Update(model);
                if (result == true)
                {
                    return Ok(new ApiResult<Customer>()
                    {
                        StatusCode = 200,
                    });
                }
                return BadRequest(new ApiResult<Customer>()
                {
                    Data = null,
                    Message = "Không thể thêm mới ",
                    StatusCode = 500,
                });
            }
            catch (Exception e)
            {
                return BadRequest(new ApiResult<Customer>()
                {
                    Data = null,
                    Message = e.Message,
                    StatusCode = 500,
                });
            }
        }
    }
}
