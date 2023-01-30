using LCS.Domain.Models;
using LCS.Domain.Repositories;
using LCS.Domain.Response;
using SimpleMediatR.MediatRContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LCS.Application.Queries.TimeSlotQ
{
    public record TimeSlotQuery:IQuery<List<TimeSlot>>;

    public class TimeSlotQueryHandler : IQueryHandler<TimeSlotQuery, List<TimeSlot>>
    {
        public async Task<List<TimeSlot>> HandlerAsync(TimeSlotQuery query, IRepoWrapper repo, CancellationToken cancellationToken = default)
        {
            return repo.TimeSlotRepo.Convertlist(await repo.TimeSlotRepo.GetAll());
        }
    }
}
