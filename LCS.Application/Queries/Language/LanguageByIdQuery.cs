using LCS.Application.Queries.DepartmentQ;
using LCS.Domain.Models;
using LCS.Domain.Repositories;
using SimpleMediatR.MediatRContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LCS.Application.Queries.LanguageQ
{
    public record LanguageByIdQuery(Guid Id):IQuery<Language?>;

    public record LanguageByIdHandler : IQueryHandler<LanguageByIdQuery, Language?>
    {
        public async Task<Language?> HandlerAsync(LanguageByIdQuery query, IRepoWrapper repo, CancellationToken cancellationToken = default)
        {
            var res = await repo.LanguageRepo.GetById(query.Id);
            if (res == null) return null;
            Language l = res;
            return l;
        }
    }
}
