using Law.Domain.Repositories;
using Utilities.ActionResponse;

namespace SimpleMediatR.MediatRContract
{
    public interface IQueryHandler< TQuery> where TQuery : IQuery
    {
       Task<ActionResult> HandlerAsync(TQuery query,IRepoWrapper repo, CancellationToken cancellationToken=default) ;
    }

    public interface IQueryHandler<TQuery,TResp> where TQuery : IQuery<TResp>
    {
        Task<TResp> HandlerAsync(TQuery query, IRepoWrapper repo, CancellationToken cancellationToken = default);
    }

}   
