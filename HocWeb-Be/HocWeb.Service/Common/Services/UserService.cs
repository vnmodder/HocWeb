using HocWeb.Infrastructure.Entities;
using HocWeb.Service.Common.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace HocWeb.Service.Common.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<User> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private string _userName;
        private int _userId;

        public UserService(UserManager<User> userManager , IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
            _userName = _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
            int.TryParse(_httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.Name), out _userId);
        }

        public string UserName => _userName ?? _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
        public string RoleName => _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.Role);
        public int UserId => _userId;

        public void DeserializeUserId(string userSerialized)
        {
            if (userSerialized == null)
            {
                return;
            }
            var userByte = Convert.FromBase64String(userSerialized);
            using var mStream = new MemoryStream(userByte);
            using var bReader = new BinaryReader(mStream);
            var claims = new ClaimsPrincipal(bReader);
            _userName = claims.FindFirstValue(ClaimTypes.NameIdentifier);
        }   
    }
}
