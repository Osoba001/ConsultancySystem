using Law.Application.Response;
using Law.Domain.Repositories;
using SimpleMediatR.MediatRContract;
using Utilities.ActionResponse;

namespace Law.Application.Queries.Client
{
    public record ClientByIdQuery(Guid Id) : IQuery;

    public class ClientByIdHandler : IQueryHandler<ClientByIdQuery>
    {
        public async Task<ActionResult> HandlerAsync(ClientByIdQuery query, IRepoWrapper repo, CancellationToken cancellationToken = default)
        {
            var res = new ActionResult();
            var client = await repo.ClientRepo.GetById(query.Id);
            if (client == null)
                res.AddError("Record is not found.");
            else
            {
                ClientResponse resp = client;
                res.data = resp;
            }
            return res;

        }
    }
}
