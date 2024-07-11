using HocWeb.Infrastructure.Entities;
using HocWeb.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace HocWeb.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpGet("get-all")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var result = await _accountService.GetAllAccounts();
                return Ok(result);
            }
            catch (Exception e)
            {
                return StatusCode(500, new { message = $"Lỗi khi lấy danh sách tài khoản: {e.Message}" });
            }
        }

        [HttpGet("get-by-id")]
        public async Task<IActionResult> GetById([FromQuery] int id)
        {
            try
            {
                var result = await _accountService.GetAccountById(id);
                if (result.Data != null)
                {
                    return Ok(result);
                }
                else
                {
                    return NotFound(result);
                }
            }
            catch (Exception e)
            {
                return StatusCode(500, new { message = $"Lỗi khi lấy thông tin tài khoản: {e.Message}" });
            }
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] Account account)
        {
            try
            {
                var result = await _accountService.CreateAccount(account, new List<string>());
                return Ok(result);
            }
            catch (Exception e)
            {
                return StatusCode(500, new { message = $"Lỗi khi tạo tài khoản: {e.Message}" });
            }
        }

        [HttpPut("update")]
        public async Task<IActionResult> Update([FromBody] Account account)
        {
            try
            {
                var result = await _accountService.UpdateAccount(account.Id, account, new List<string>());
                if (result.Data != null)
                {
                    return Ok(result);
                }
                else
                {
                    return NotFound(result);
                }
            }
            catch (Exception e)
            {
                return StatusCode(500, new { message = $"Lỗi khi cập nhật tài khoản: {e.Message}" });
            }
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> Delete([FromQuery] int id)
        {
            try
            {
                var result = await _accountService.DeleteAccount(id, new List<string>());
                return Ok(result);
            }
            catch (Exception e)
            {
                return StatusCode(500, new { message = $"Lỗi khi xóa tài khoản: {e.Message}" });
            }
        }

        [Authorize]
        [HttpGet("check-access")]
        public async Task<IActionResult> CheckAccess([FromQuery] int accountId)
        {
            try
            {
                var result = await _accountService.CheckAccess(accountId, new List<string>());
                if (result.Data is bool isAdmin && isAdmin)
                {
                    return Ok(new { message = "Bạn có quyền truy cập." });
                }
                else
                {
                    return Forbid();
                }
            }
            catch (Exception e)
            {
                return StatusCode(500, new { message = $"Lỗi khi kiểm tra quyền truy cập: {e.Message}" });
            }
        }
    }
}
