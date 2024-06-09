using HocWeb.Infrastructure.Entities;
using HocWeb.Service.Interfaces;
using HocWeb.Service.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HocWeb.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SupplierController : ControllerBase
    {
        private readonly ISupplierService _supplierService;

        public SupplierController(ISupplierService supplierService)
        {
            _supplierService = supplierService;
        }

        [HttpGet("get-all")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var result = await _supplierService.GetAll();
                return Ok(new ApiResult<IList<Supplier>>()
                {
                    Data = result,
                    StatusCode = 200,
                });
            }
            catch (Exception e)
            {
                return BadRequest(new ApiResult<IList<Supplier>>()
                {
                    Data = null,
                    Message = e.Message,
                    StatusCode = 500,
                });
            }
        }

        [HttpGet("get-by-id")]
        public async Task<IActionResult> GetById([FromQuery] int id)
        {
            try
            {
                var result = await _supplierService.GetById(id);
                if (result != null)
                {
                    return Ok(new ApiResult<Supplier>()
                    {
                        Data = result,
                        StatusCode = 200,
                    });
                }
                return BadRequest(new ApiResult<Supplier>()
                {
                    Data = null,
                    StatusCode = 500,
                });
            }
            catch (Exception e)
            {
                return BadRequest(new ApiResult<Supplier>()
                {
                    Data = null,
                    Message = e.Message,
                    StatusCode = 500,
                });
            }
        }

        [Authorize]
        [HttpPost("add")]
        public async Task<IActionResult> Add([FromBody] Supplier model)
        {
            try
            {
                var result = await _supplierService.Add(model);
                if (result != null)
                {
                    return Ok(new ApiResult<Supplier>()
                    {
                        Data = result,
                        StatusCode = 200,
                    });
                }
                return BadRequest(new ApiResult<Supplier>()
                {
                    Data = null,
                    Message = "Không thể thêm!.",
                    StatusCode = 500,
                });
            }
            catch (Exception e)
            {
                return BadRequest(new ApiResult<Supplier>()
                {
                    Data = null,
                    Message = e.Message,
                    StatusCode = 500,
                });
            }
        }

        [Authorize]
        [HttpPut("update")]
        public async Task<IActionResult> Update([FromBody] Supplier model)
        {
            try
            {
                var result = await _supplierService.Update(model);
                if (result)
                {
                    return Ok(new ApiResult<Supplier>()
                    {
                        Data = model,
                        StatusCode = 200,
                    });
                }
                return BadRequest(new ApiResult<Supplier>()
                {
                    Data = null,
                    Message = "Không thể cập nhật!.",
                    StatusCode = 500,
                });
            }
            catch (Exception e)
            {
                return BadRequest(new ApiResult<Supplier>()
                {
                    Data = null,
                    Message = e.Message,
                    StatusCode = 500,
                });
            }
        }

        [Authorize]
        [HttpDelete("delete")]
        public async Task<IActionResult> Delete([FromQuery] int id)
        {
            try
            {
                var result = await _supplierService.Delete(id);
                if (result)
                {
                    return Ok(new ApiResult<object>(null)
                    {
                        StatusCode = 200,
                        Message = "Xóa thành công!"
                    });
                }
                return BadRequest(new ApiResult<object>(null)
                {
                    Message = "Xóa thất bại!",
                    StatusCode = 500,
                });
            }
            catch (Exception e)
            {
                return BadRequest(new ApiResult<object>(null)
                {
                    Message = e.Message,
                    StatusCode = 500,
                });
            }
        }
    }
}
