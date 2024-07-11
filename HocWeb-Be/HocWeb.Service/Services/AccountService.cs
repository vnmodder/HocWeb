using HocWeb.Infrastructure;
using HocWeb.Infrastructure.Constants;
using HocWeb.Infrastructure.Entities;
using HocWeb.Service.Common.IServices;
using HocWeb.Service.Interfaces;
using HocWeb.Service.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HocWeb.Service.Services
{
    public class AccountService : BaseService, IAccountService
    {
        private readonly UserManager<Account> _userManager;

        public AccountService(DataContext dataContext, IUserService userService, UserManager<Account> userManager)
            : base(dataContext, userService)
        {
            _userManager = userManager;
        }   
        public async Task<ApiResult> GetAllAccounts()
        {
            try
            {
                var accounts = await _dataContext.Accounts.ToListAsync();
                return new ApiResult(accounts);
            }
            catch (Exception e)
            {
                throw new Exception($"Lỗi: {e.Message}");
            }
        }

        public async Task<ApiResult> GetAccountById(int accountId)
        {
            try
            {
                var account = await _dataContext.Accounts.FirstOrDefaultAsync(a => a.Id == accountId);
                if (account != null)
                {
                    return new ApiResult(account);
                }
                else
                {
                    return new ApiResult { Message = "Không tìm thấy tài khoản" };
                }
            }
            catch (Exception e)
            {
                throw new Exception($"Lỗi khi lấy thông tin tài khoản: {e.Message}");
            }
        }

        public async Task<ApiResult> CreateAccount(Account account, List<string> roles)
        {
            using var tran = await _dataContext.Database.BeginTransactionAsync();
            try
            {
                account.CreatedDate = _now;
                _dataContext.Accounts.Add(account);
                await _dataContext.SaveChangesAsync();

                await tran.CommitAsync();
                return new ApiResult(account);
            }
            catch (Exception e)
            {
                await tran.RollbackAsync();
                throw new Exception($"Lỗi khi tạo tài khoản: {e.Message}");
            }
        }

        public async Task<ApiResult> UpdateAccount(int accountId, Account account, List<string> roles)
        {
            using var tran = await _dataContext.Database.BeginTransactionAsync();
            try
            {
                var existingAccount = await _dataContext.Accounts.FirstOrDefaultAsync(a => a.Id == accountId);
                if (existingAccount == null)
                {
                    throw new Exception("Không tìm thấy tài khoản");
                }

                existingAccount.Username = account.Username ?? existingAccount.Username;
                existingAccount.Password = account.Password ?? existingAccount.Password;
                existingAccount.Type = account.Type ?? existingAccount.Type;
                existingAccount.Email = account.Email ?? existingAccount.Email;
                existingAccount.FullName = account.FullName ?? existingAccount.FullName;
                existingAccount.AvatarUrl = account.AvatarUrl ?? existingAccount.AvatarUrl;
                existingAccount.CreatedDate = account.CreatedDate ?? existingAccount.CreatedDate;

                await _dataContext.SaveChangesAsync();

                await tran.CommitAsync();
                return new ApiResult(existingAccount);
            }
            catch (Exception e)
            {
                await tran.RollbackAsync();
                throw new Exception($"Lỗi khi cập nhật tài khoản: {e.Message}");
            }
        }

        public async Task<ApiResult> DeleteAccount(int accountId, List<string> roles)
        {
            using var tran = await _dataContext.Database.BeginTransactionAsync();
            try
            {
                var account = await _dataContext.Accounts.FirstOrDefaultAsync(a => a.Id == accountId);
                if (account == null)
                {
                    throw new Exception("Không tìm thấy tài khoản");
                }

                _dataContext.Accounts.Remove(account);
                await _dataContext.SaveChangesAsync();

                await tran.CommitAsync();
                return new ApiResult();
            }
            catch (Exception e)
            {
                await tran.RollbackAsync();
                throw new Exception($"Lỗi khi xóa tài khoản: {e.Message}");
            }
        }


        public async Task<ApiResult> CheckAccess(int accountId, List<string> roles)
        {
            try
            {
                var account = await _dataContext.Accounts.FirstOrDefaultAsync(a => a.Id == accountId);
                if (account == null)
                {
                    return new ApiResult { Message = "Không tìm thấy tài khoản" };
                }

                var userRoles = await _userManager.GetRolesAsync(account);
                bool isAdmin = userRoles.Contains(RoleConstants.ADMIN);

                if (isAdmin)
                {
                    return new ApiResult(true);
                }
                else
                {
                    return new ApiResult { Message = "Không có quyền truy cập" };
                }
            }
            catch (Exception e)
            {
                throw new Exception($"Lỗi: {e.Message}");
            }
        }
    }
}
