using LCS.Domain.Models;
using LCS.Domain.Repositories;
using SimpleMediatR.MediatRContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LCS.Application.Queries.LawyerQ
{
    public record AllLawyerQuery:IQuery<List<Lawyer>>;

    public class AllLawyerHandler : IQueryHandler<AllLawyerQuery, List<Lawyer>>
    {
        public async Task<List<Lawyer>> HandlerAsync(AllLawyerQuery query, IRepoWrapper repo, CancellationToken cancellationToken = default)
        {
            return repo.LawyerRepo.Convertlist(await repo.LawyerRepo.GetAll());
        }
    }
}
