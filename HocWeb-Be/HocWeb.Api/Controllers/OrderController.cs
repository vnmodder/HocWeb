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
    public class OrderController : ControllerBase
    {

        private readonly IOrderService orderService;

        public OrderController(IOrderService orderService)
        {
            this.orderService = orderService;
        }
        [HttpGet("get-all")]
        public async Task<IActionResult> GetAll()
        {
           var result = await orderService.GetAll();
            return Ok(new ApiResult<IList<Order>>()
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
                var result = await orderService.GetById(id);
                if (result != null)
                    return Ok(new ApiResult<Order>()
                    {
                        Data = result,
                        StatusCode = 200,
                    });
                return BadRequest(new ApiResult<Order>()
                {
                    Data = null,
                    StatusCode = 500,
                });
            }
            catch (Exception e)
            {
                return BadRequest(new ApiResult<Order>()
                {
                    Data = null,
                    Message = e.Message,
                    StatusCode = 500,
                });
            }
        }
        [Authorize]
        [HttpPost("add")]
        public async Task<IActionResult> Add([FromBody] Order model)
        {
            try
            {
                var result = await orderService.Add(model);
                return Ok(new ApiResult<Order>()
                {
                    Data = result,
                    StatusCode = 200,
                });
            }
            catch (Exception e)
            {
                return BadRequest(new ApiResult<Order>()
                {
                    Data = null,
                    Message = e.Message,
                    StatusCode = 500,
                });
            }
        }
        /*[Authorize]
        [HttpDelete("delete")]
        public async Task<IActionResult> Delete([FromQuery] int id)
        {
            try
            {
                bool result = await orderService.Delete(id);
                if (result)
                {
                    return Ok(new ApiResult<Order>()
                    {
                        Data = result,
                        StatusCode = 200,
                    }); ;
                }
                else
                {
                    return BadRequest(new ApiResult<Order>()
                    {
                        Data = null,
                        Message = "Không thể xóa",
                        StatusCode = 500,
                    });
                }
            }
            catch (Exception e)
            {
                return BadRequest(new ApiResult<Order>()
                {
                    Data = null,
                    Message = e.Message,
                    StatusCode = 500,
                });
            }
        }*/
/*
        [HttpPatch("update")]
        public async Task<IActionResult> Update([FromBody] Order model)
        {
            try
            {
                var result = await orderService.Update(model);
                return Ok(new ApiResult<Order>()
                {
                    Data = result,
                    StatusCode = 200,
                });
            }
            catch (Exception e)
            {
                return BadRequest(new ApiResult<Order>()
                {
                    Data = null,
                    Message = e.Message,
                    StatusCode = 500,
                });
            }
        }*/
    }
}
