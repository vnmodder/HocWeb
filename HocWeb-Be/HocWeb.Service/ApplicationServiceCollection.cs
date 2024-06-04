using HocWeb.Infrastructure.Entities;
using HocWeb.Service.Interfaces;
using HocWeb.Service.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace HocWeb.Service
{
    public static class ApplicationServiceCollection
    {
        /// <summary>
        /// Adds the application services.
        /// </summary>
        /// <param name="services">The services.</param>
        /// <returns></returns>
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            #region Common services
            // JWT
            services.AddScoped<IJwtService, JwtService>();

            // User Management Service.
            services.AddScoped<UserManager<User>>();
            services.AddScoped<SignInManager<User>, SignInManager<User>>();

            #endregion

            #region Business services
            services.AddScoped<IAuthenticateService, AuthenticateService>();
            #endregion

            return services;
        }
    }
}
