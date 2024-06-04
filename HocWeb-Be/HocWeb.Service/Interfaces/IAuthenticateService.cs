using HocWeb.Service.Models;
using HocWeb.Service.Models.Authenticate;

namespace HocWeb.Service.Interfaces
{
    public interface IAuthenticateService
    {
        Task<ApiResult<string>> Register(RegisterModel model);
        Task<ApiResult<UserToken>> Login(LoginModel model);
    }
}
