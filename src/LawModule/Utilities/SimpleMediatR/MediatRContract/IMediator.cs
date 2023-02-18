using Utilities.ActionResponse;

namespace SimpleMediatR.MediatRContract
{
    public interface IMediator
    {
        Task<ActionResult> ExecuteCommandAsync<TCommandHandler, TCommand>(TCommand command)
            where TCommand : ICommand
            where TCommandHandler : ICommandHandler<TCommand>;

        Task<ActionResult> QueryNullableAsync<TQueryHandler, TQuery>(TQuery query)
            where TQuery : IQuery
            where TQueryHandler : IQueryHandler<TQuery>;

        Task<object> QueryAsync<TQueryHandler, TQuery>(TQuery query)
            where TQuery : IQuery
            where TQueryHandler : QueryHandler<TQuery>;
        IServiceProvider ServiceProvider { get; }
    }

}   
