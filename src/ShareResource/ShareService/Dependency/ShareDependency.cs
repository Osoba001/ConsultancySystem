using Microsoft.Extensions.DependencyInjection;
using ShareServices.EmailService;
using ShareServices.RedisService;

namespace ShareServices.Dependency
{
    public static class ShareDependency
    {
        public static IServiceCollection ShareServiceCollection(this IServiceCollection services)
        {
            services.AddTransient<IEmailSender,EmaiKitlSender>();
            services.AddTransient<IRedisDatabase, RedisDatabase>();
            return services;
        }
    }
}
