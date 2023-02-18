using Law.Domain.Repositories;
using Utilities.ActionResponse;

namespace SimpleMediatR.MediatRContract
{
    public interface IQueryHandler< TQuery> where TQuery : IQuery
    {
       Task<ActionResult> HandlerAsync(TQuery query,IRepoWrapper repo, CancellationToken cancellationToken=default) ;
    }

    public abstract class QueryHandler<TQuery> where TQuery : IQuery
    {
       public abstract  Task<object> HandlerAsync(TQuery query, IRepoWrapper repo, CancellationToken cancellationToken = default);
    }
}   
