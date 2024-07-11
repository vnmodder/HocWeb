using HocWeb.Infrastructure.Entities;
using HocWeb.Service.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HocWeb.Service.Interfaces
{
    public interface IAccountService
    {
        Task<ApiResult> GetAllAccounts();
        Task<ApiResult> GetAccountById(int accountId);
        Task<ApiResult> CreateAccount(Account account, List<string> roles);
        Task<ApiResult> UpdateAccount(int accountId, Account account, List<string> roles);
        Task<ApiResult> DeleteAccount(int accountId, List<string> roles);
        Task<ApiResult> CheckAccess(int accountId, List<string> roles);
    }
}
