using Law.Application.Commands.AppointmentB;
using Law.Application.EventService;
using Law.Domain.Repositories;
using Law.Persistence.Repositories;
using Microsoft.Extensions.DependencyInjection;
using ShareServices.Events;
using SimpleMediatR.MediatR;
using SimpleMediatR.MediatRContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Law.Persistence.Dependency
{
    public static class LawDependency
    {
        public static IServiceCollection LawDependencyCollection(this IServiceCollection services)
        {
            services.AddScoped<IMediator, Mediator>();
            services.AddScoped<IRepoWrapper, RepoWrapper>();
            services.AddScoped<ILawModuleEventService, UserEventService>();
            return services;
        }
    }
}
