using Auth.AuthServices;
using Auth.Data;
using Auth.Entities;
using Auth.Models;
using Auth.Repository;
using Auth.UserServices;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.AuthConfigurations
{
    public static class AuthConfigurationService
    {
       
        public static IServiceCollection AuthConfigService(this IServiceCollection services, string secret)
        {

            services.AuthenticationSetup(secret);
            services.AuthService();
            return services;
        }
        private  static IServiceCollection AuthenticationSetup(this IServiceCollection services, string secretKey)
        {
            var key = Encoding.ASCII.GetBytes(secretKey);
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = true;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key)
                };
            });
            return services;
        }
        //public void DbContextService()
        //{
        //    //_services.AddSqlServer<AuthDbContext>(_connString, op =>
        //    //{
        //    //    op.EnableRetryOnFailure().CommandTimeout(60).MaxBatchSize(2);
        //    //});
        //}
        public static IServiceCollection AuthService(this IServiceCollection services)
        {
            services.AddTransient<IUserService,UserService>();
            services.AddTransient<IUserRoleService, UserRoleService>();
            services.AddTransient<IUserRepo, UserRepo>();
            services.AddTransient<IUserRoleRepo,UserRoleRepo>();
            services.AddTransient<IAssignedUserRoleRepo,AssignedUserRoleRepo>();
            services.AddTransient<IAuthService, AuthService>();
            return services;
        }
    }
}
