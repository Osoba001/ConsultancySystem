using Law.Application.Helpers;
using Law.Application.Response;
using Law.Domain.Repositories;
using SimpleMediatR.MediatRContract;

namespace Law.Application.Queries.TimeSlotQ
{
    public class TimeSlotQuery : IQuery { }

    public class TimeSlotQueryHandler : QueryHandler<TimeSlotQuery>
    {
        public override async Task<object> HandlerAsync(TimeSlotQuery query, IRepoWrapper repo, CancellationToken cancellationToken = default)
        {
            return (await repo.TimeSlotRepo.GetAll()).TimeSlotListConv();
        }
    }
}
