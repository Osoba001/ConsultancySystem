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
    public record LawyerByIdQuery(Guid Id):IQuery<Lawyer?>;

    public class LawyerByIdQueryHandler : IQueryHandler<LawyerByIdQuery, Lawyer?>
    {
        public async Task<Lawyer?> HandlerAsync(LawyerByIdQuery query, IRepoWrapper repo, CancellationToken cancellationToken = default)
        {
            var lawyer = await repo.LawyerRepo.GetById(query.Id);
            if (lawyer == null) 
                return null;
            Lawyer lyr= lawyer;
            return lyr;

        }
    }
}
