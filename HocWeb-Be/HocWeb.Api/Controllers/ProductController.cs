﻿using HocWeb.Infrastructure.Entities;
using HocWeb.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using HocWeb.Service.Models.Product;

namespace HocWeb.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : BaseController
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet("get-all")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _productService.GetAll();
            return Response(result);
        }

        [HttpGet("getAll_insist_deleted")]
        public async Task<IActionResult> GetAll_Insist_Deleted()
        {
            var result = await _productService.GetAll_Insist_Deleted();
            return Response(result);
        }
            

        [HttpGet("get-by-id")]
        public async Task<IActionResult> GetById([FromQuery] int id)
        {
            try
            {
                var result = await _productService.GetById(id);
                return Response(result);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        [Authorize]
        [HttpPost("add")]
        public async Task<IActionResult> Add([FromBody] Product model)
        {
            try
            {
                var result = await _productService.Add(model);
                return Response(result);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        [Authorize]
        [HttpPost("add2")]
        public async Task<IActionResult> Add([FromForm] AddProductModel model)
        {
            try
            {
                var result = await _productService.Add2(model);
                return Response(result);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        [Authorize]
        [HttpPut("update")]
        public async Task<IActionResult> Update([FromBody] Product model)
        {
            try
            {
                var result = await _productService.Update(model);
                return Response(result);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        [Authorize]
        [HttpDelete("delete")]
        public async Task<IActionResult> Delete([FromQuery] int id)
        {
            try
            {
                var result = await _productService.Delete(id);
                return Response(result);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        [Authorize]
        [HttpPost("delete_unpermanently")]
        public async Task<IActionResult> Delete_Unpermanently([FromBody] int id)
        {
            try
            {
                var result = await _productService.Delete_Unpermanently(id);
                return Response(result);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        [Authorize]
        [HttpPost("cancel_delete_unpermanently")]
        public async Task<IActionResult> Cancel_Delete_Unpermanently([FromBody] int id)
        {
            try
            {
                var result = await _productService.Cancel_Delete_Unpermanently(id);
                return Response(result);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
