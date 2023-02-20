using Microsoft.Extensions.DependencyInjection;
using ShareServices.AsDatabase;
using ShareServices.ASMessages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShareServices.Dependency
{
    public static class ShareDependency
    {
        public static IServiceCollection ShareServiceCollection(this IServiceCollection services)
        {
            services.AddTransient<IEmailSender,EmaiKitlSender>();
            services.AddTransient<IRedisMsg, Messages>();
            services.AddTransient<IRedisDatabase, RedisDatabase>();
            return services;
        }
    }
}
