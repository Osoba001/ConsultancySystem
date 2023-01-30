using LCS.Domain.Models;
using LCS.Domain.Repositories;
using LCS.Domain.Response;

namespace SimpleMediatR.MediatRContract
{
    public interface IQueryHandler< TQuery,TResp> where TQuery : IQuery<TResp>
    {
       public abstract Task<TResp> HandlerAsync(TQuery query,IRepoWrapper repo, CancellationToken cancellationToken=default) ;
    }


}   
