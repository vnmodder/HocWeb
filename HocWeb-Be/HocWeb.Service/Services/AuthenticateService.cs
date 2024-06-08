﻿using HocWeb.Infrastructure;
using HocWeb.Infrastructure.Constants;
using HocWeb.Infrastructure.Entities;
using HocWeb.Service.Interfaces;
using HocWeb.Service.Models;
using HocWeb.Service.Models.Authenticate;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace HocWeb.Service.Services
{
    public class AuthenticateService : IAuthenticateService
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly DataContext _dbContext;
        private readonly RoleManager<IdentityRole<int>> _roleManager;
        private readonly IConfiguration _configuration;

        public AuthenticateService(
            DataContext dbContext,
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            RoleManager<IdentityRole<int>> roleManager,
            IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
            _roleManager = roleManager;
            _dbContext = dbContext;
        }

        public async Task<ApiResult<UserToken>> Login(LoginModel model)
        {
            try
            {
                var user = await _userManager.FindByNameAsync(model.UserName);

                if (user != null && !user.DeleteDate.HasValue)
                {
                    var signInResult = await _signInManager.PasswordSignInAsync(model.UserName, model.Password, false, lockoutOnFailure: false);
                    if (signInResult.Succeeded)
                    {

                        var role = await _userManager.GetRolesAsync(user);
                        var roleString = String.Join(",", role.ToArray());
                        var tokenResult = GenerateUserToken(user, roleString);
                        if (tokenResult != null)
                        {
                            return new ApiResult<UserToken>()
                            {
                                Data = tokenResult,
                                Message = "Success",
                                StatusCode = 200
                            };
                        }
                    }

                }

                return new ApiResult<UserToken>()
                {
                    Data = null,
                    Message = "Unorthorize",
                    StatusCode = 403
                };
            }
            catch (Exception e)
            {
                throw new Exception(e.ToString());
            }
        }

        public async Task<ApiResult<string>> Register(RegisterModel model)
        {
            using var transaction = await _dbContext.Database.BeginTransactionAsync();
            try
            {
                var userExist = await _userManager.FindByNameAsync(model.UserName);
                if (userExist != null)
                {
                    return new ApiResult<string>()
                    {
                        StatusCode = 400,
                        Message = $"{model.Email}  đã được sử dụng. Vui lòng thử lại!"
                    }; ;
                }

                User user = new User()
                {
                    UserName = model.UserName,
                    Email = model.Email,
                    FullName = model.FullName,
                    SecurityStamp = Guid.NewGuid().ToString(),
                };

                var newUserResult = await _userManager.CreateAsync(user, model.Password);

                if (!newUserResult.Succeeded)
                {
                    return new ApiResult<string>()
                    {
                        StatusCode = 500,
                        Message = "User creation failed"
                    };
                }

                if (!await _roleManager.RoleExistsAsync(RoleConstants.USER.ToString()))
                {
                    await _roleManager.CreateAsync(new IdentityRole<int>(RoleConstants.USER.ToString()));
                }

                await _userManager.AddToRoleAsync(user, RoleConstants.USER.ToString());

                await _dbContext.SaveChangesAsync();
                await transaction.CommitAsync();

                return new ApiResult<string>()
                {
                    Message = "Success",
                    StatusCode = 200
                };

            }
            catch (Exception e)
            {
                await transaction.RollbackAsync();
                throw new Exception(e.ToString());
            }
        }


        private UserToken GenerateUserToken(User user, string role, bool isExternalLogin = false)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Secret"]);

            var expires = DateTime.UtcNow.AddDays(7);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                    new Claim("user-name", user.UserName?.ToString()),
                    new Claim("role",role),
                    new Claim(ClaimTypes.NameIdentifier, user.UserName),
                    new Claim(ClaimTypes.Email, user.Email),
                }),

                Expires = expires,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256),
                Issuer = _configuration["Jwt:ValidIssuer"],
                Audience = _configuration["Jwt:ValidAudience"]
            };

            var securityToken = tokenHandler.CreateToken(tokenDescriptor);

            var token = tokenHandler.WriteToken(securityToken);

            return new UserToken
            {
                UserId = user.Id,
                Email = user.Email,
                Token = token,
                Expires = expires
            };
        }
    }
}
