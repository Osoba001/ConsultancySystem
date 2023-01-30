using LCS.Domain.Models;
using LCS.Domain.Repositories;
using SimpleMediatR.MediatRContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LCS.Application.Queries.ClientQ
{
    public record AllClientQuery:IQuery<List<Client>>;

    public class AllClientHandler : IQueryHandler<AllClientQuery, List<Client>>
    {
        public async Task<List<Client>> HandlerAsync(AllClientQuery query, IRepoWrapper repo, CancellationToken cancellationToken = default)
        {
            return repo.ClientRepo.Convertlist(await repo.ClientRepo.GetAll());
        }
    }
}
