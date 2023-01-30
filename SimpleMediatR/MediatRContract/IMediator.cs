using LCS.Domain.Response;

namespace SimpleMediatR.MediatRContract
{
    public interface IMediator
    {
        Task<ActionResult> ExecuteCommandAsync<TCommandHandler, TCommand>(TCommand command)
            where TCommand : ICommand
            where TCommandHandler : ICommandHandler<TCommand>;

        Task<TResp> SendQueryAsync<TQueryHandler, TQuery, TResp>(TQuery query)
            where TQuery : IQuery<TResp>
            where TQueryHandler : IQueryHandler<TQuery,TResp>;
            
        IServiceProvider ServiceProvider { get; }
    }

}   
