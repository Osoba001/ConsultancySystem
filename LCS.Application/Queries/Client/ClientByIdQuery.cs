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
    public record ClientByIdQuery(Guid Id):IQuery<Client?>;

    public class ClientByIdHandler : IQueryHandler<ClientByIdQuery, Client?>
    {
        public async Task<Client?> HandlerAsync(ClientByIdQuery query, IRepoWrapper repo, CancellationToken cancellationToken = default)
        {
            var client= await repo.ClientRepo.GetById(query.Id);
            if (client == null)
                return null;
            Client cl=client;
            return cl;

        }
    }
}
