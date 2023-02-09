using LCS.Application.Queries.AppointmentQ;
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
    public record DepartmentByIdQuery(Guid Id):IQuery<Department?>;

    public record DepartmentByIdHandler : IQueryHandler<DepartmentByIdQuery, Department?>
    {
        public async Task<Department?> HandlerAsync(DepartmentByIdQuery query, IRepoWrapper repo, CancellationToken cancellationToken = default)
        {
            var res = await repo.DepartmentRepo.GetById(query.Id);
            if (res == null) return null;
            Department d = res;
            return d;
        }
    }
}
