using Law.Application.Helpers;
using Law.Application.Response;
using Law.Domain.Repositories;
using SimpleMediatR.MediatRContract;

namespace Law.Application.Queries.Client
{
    public record AllClientQuery : IQuery<List<ClientResponse>>;

    public class AllClientHandler : IQueryHandler<AllClientQuery, List<ClientResponse>>
    {
        public async Task<List<ClientResponse>> HandlerAsync(AllClientQuery query, IRepoWrapper repo, CancellationToken cancellationToken = default)
        {
            return (await repo.ClientRepo.GetAll()).ClientListConv();
        }
    }
}
