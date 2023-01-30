using LCS.Domain.Models;
using LCS.Domain.Repositories;
using SimpleMediatR.MediatRContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LCS.Application.Queries.DepartmentQ
{
    public record AllDepartmentQuery:IQuery<List<Department>>;

    public class AllDepartmentHandler : IQueryHandler<AllDepartmentQuery, List<Department>>
    {
        public async Task<List<Department>> HandlerAsync(AllDepartmentQuery query, IRepoWrapper repo, CancellationToken cancellationToken = default)
        {
            return repo.DepartmentRepo.Convertlist(await repo.DepartmentRepo.GetAll());
        }

       
    }
}
