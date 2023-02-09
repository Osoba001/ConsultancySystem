using LCS.Domain.Repositories;
using LCS.Domain.Response;
using SimpleMediatR.MediatRContract;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleMediatR.MediatR
{
    public class Mediator : IMediator
    {
        public Mediator(IServiceProvider serviceProvider)
        {
            ServiceProvider=serviceProvider;
        }
        protected IRepoWrapper RepoWrapper => ServiceProvider.GetRequiredService<IRepoWrapper>();

        public IServiceProvider ServiceProvider { get; }
        public Task<ActionResult> ExecuteCommandAsync<TCommandHandler, TCommand>(TCommand command)
            where TCommandHandler : ICommandHandler<TCommand>
            where TCommand : ICommand
        {
            var handler=(TCommandHandler)Activator.CreateInstance(typeof(TCommandHandler));
            return handler.HandleAsync(command, RepoWrapper);
        }

        public Task<TResp> SendQueryAsync<TQueryHandler, TQuery, TResp>(TQuery query)
            where TQueryHandler : IQueryHandler<TQuery,TResp>
            where TQuery : IQuery<TResp>
        {
            var handler=(TQueryHandler)Activator.CreateInstance(typeof(TQuery))!;
            return handler.HandlerAsync(query, RepoWrapper);    
        }

    }
}
