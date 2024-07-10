using HocWeb.Service.Common.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace HocWeb.Api.Controllers
{
    [Route("files")]
    [ApiController]
    public class FileController : ControllerBase
    {
        private readonly IFtpDirectoryService _ftpDirectoryService;
        public FileController(IFtpDirectoryService ftpDirectoryService) {
            _ftpDirectoryService = ftpDirectoryService;
        }

        [HttpGet("public/{*filePath}")]
        public async Task<IActionResult> GetPublicFile(string filePath)
        {
            var deCodeFIlePath = WebUtility.UrlDecode(filePath);
            var result = _ftpDirectoryService.GetFileStream($"public/{deCodeFIlePath}");
            if(result.Succeed)
            {
                var fileStream = result.FileStream;
                fileStream.Position = 0; 

                var contentType = "application/octet-stream";
                var fileName = Path.GetFileName(deCodeFIlePath);
                return File(fileStream, contentType, fileName);
            }

            return NotFound();
        }

        [HttpGet("{*filePath}")]
        [Authorize]

        public async Task<IActionResult> GetFile(string filePath)
        {
            var deCodeFIlePath = WebUtility.UrlDecode(filePath);
            var result = _ftpDirectoryService.GetFileStream($"{deCodeFIlePath}");
            if (result.Succeed)
            {
                var fileStream = result.FileStream;
                fileStream.Position = 0;

                var contentType = "application/octet-stream";
                var fileName = Path.GetFileName(deCodeFIlePath);
                return File(fileStream, contentType, fileName);
            }

            return NotFound();
        }
    }
}
