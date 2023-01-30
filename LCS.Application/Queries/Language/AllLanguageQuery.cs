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
    public record AllLanguageQuery:IQuery<List<Language>>;

    public class AllLanguageHandler : IQueryHandler<AllLanguageQuery, List<Language>>
    {
        public async Task<List<Language>> HandlerAsync(AllLanguageQuery query, IRepoWrapper repo, CancellationToken cancellationToken = default)
        {
            return repo.LanguageRepo.Convertlist(await repo.LanguageRepo.GetAll());
        }
    }
}
