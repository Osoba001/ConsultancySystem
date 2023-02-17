using Auth.UserServices;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using User.Application.AuthServices;
using User.Application.Repository;
using User.Application.UserServices;
using User.Persistence.Repositories;

namespace User.Persistence.Dependency
{
    public static class AuthConfigurationService
    {

        public static IServiceCollection AuthConfigService(this IServiceCollection services, string secret)
        {

            services.AuthenticationSetup(secret);
            services.AuthService();
            return services;
        }
        private static IServiceCollection AuthenticationSetup(this IServiceCollection services, string secretKey)
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
        public static IServiceCollection AuthService(this IServiceCollection services)
        {
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IUserRepo, UserRepo>();
            services.AddTransient<IAuthService, AuthService>();
            return services;
        }
    }
}
