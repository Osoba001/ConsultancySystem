using Law.Application.Helpers;
using Law.Application.Response;
using Law.Domain.Repositories;
using SimpleMediatR.MediatRContract;

namespace Law.Application.Queries.Client
{
    public record AllClientQuery : IQuery;

    public class AllClientHandler : QueryHandler<AllClientQuery>
    {
        public override async Task<object> HandlerAsync(AllClientQuery query, IRepoWrapper repo, CancellationToken cancellationToken = default)
        {
            return (await repo.ClientRepo.GetAll()).ClientListConv();
        }
    }
}
