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
    public record LawyerByDepartmentQuery(Guid DepartmentId):IQuery<List<Lawyer>>;

    public class LawyerByDepartmentHandler : IQueryHandler<LawyerByDepartmentQuery, List<Lawyer>>
    {
        public async Task<List<Lawyer>> HandlerAsync(LawyerByDepartmentQuery query, IRepoWrapper repo, CancellationToken cancellationToken = default)
        {
            var dept = await repo.DepartmentRepo.GetById(query.DepartmentId);
            if (dept is null)
                return new List<Lawyer>();
            var lawyers = from l in dept.JoinedDepartments select l.Lawyer;
            return repo.LawyerRepo.Convertlist(lawyers.ToList());

        }
    }
}
