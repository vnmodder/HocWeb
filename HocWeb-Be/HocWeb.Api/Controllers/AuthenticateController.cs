using HocWeb.Service.Interfaces;
using HocWeb.Service.Models;
using HocWeb.Service.Models.Authenticate;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HocWeb.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticateController : ControllerBase
    {
        private readonly IAuthenticateService _authenticateService;

        public AuthenticateController(IAuthenticateService authenticateService)
        {
            _authenticateService = authenticateService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginModel request)
        {
            try
            {
                var result = await _authenticateService.Login(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterModel request)
        {
            try
            {
                var result = await _authenticateService.Register(request);
                return new JsonResult(result.Data)
                {
                    StatusCode = result.StatusCode,
                    Value = result.Message
                };
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
