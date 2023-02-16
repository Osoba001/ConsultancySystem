using Utilities.ActionResponse;

namespace SimpleMediatR.MediatRContract
{
    public interface IMediator
    {
        Task<ActionResult> ExecuteCommandAsync<TCommandHandler, TCommand>(TCommand command)
            where TCommand : ICommand
            where TCommandHandler : ICommandHandler<TCommand>;

        Task<ActionResult> QueryAsync<TQueryHandler, TQuery>(TQuery query)
            where TQuery : IQuery
            where TQueryHandler : IQueryHandler<TQuery>;

        Task<TResp> QueryAsync<TQueryHandler, TQuery,TResp>(TQuery query)
            where TQuery : IQuery<TResp>
            where TQueryHandler : IQueryHandler<TQuery,TResp>;
        IServiceProvider ServiceProvider { get; }
    }

}   
