using Microsoft.Extensions.DependencyInjection;
using ShareServices.EmailService;

namespace ShareServices.Dependency
{
    public static class ShareDependency
    {
        public static IServiceCollection ShareServiceCollection(this IServiceCollection services)
        {
            services.AddTransient<IEmailSender,EmaiKitlSender>();
            return services;
        }
    }
}
