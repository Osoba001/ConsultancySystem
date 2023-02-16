using SimpleMediatR.MediatRContract;
using Microsoft.Extensions.DependencyInjection;
using Utilities.ActionResponse;
using Law.Domain.Repositories;

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
            var handler=(TCommandHandler)Activator.CreateInstance(typeof(TCommandHandler))!;
            return handler.HandleAsync(command, RepoWrapper);
        }
        public Task<TResp> QueryAsync<TQueryHandler, TQuery, TResp>(TQuery query)
           where TQueryHandler : IQueryHandler<TQuery,TResp>
           where TQuery : IQuery<TResp>
        {
            var handler = (TQueryHandler)Activator.CreateInstance(typeof(TQuery))!;
            return handler.HandlerAsync(query, RepoWrapper);
        }
        public Task<ActionResult> QueryAsync<TQueryHandler, TQuery>(TQuery query)
            where TQueryHandler : IQueryHandler<TQuery>
            where TQuery : IQuery
        {
            var handler=(TQueryHandler)Activator.CreateInstance(typeof(TQuery))!;
            return handler.HandlerAsync(query, RepoWrapper);    
        }
    }
}
