using Law.Application.Helpers;
using Law.Application.Response;
using Law.Domain.Repositories;
using SimpleMediatR.MediatRContract;

namespace Law.Application.Queries.TimeSlotQ
{
    public record TimeSlotQuery : IQuery<List<TimeSlotResponse>>;

    public class TimeSlotQueryHandler : IQueryHandler<TimeSlotQuery, List<TimeSlotResponse>>
    {
        public async Task<List<TimeSlotResponse>> HandlerAsync(TimeSlotQuery query, IRepoWrapper repo, CancellationToken cancellationToken = default)
        {
            return (await repo.TimeSlotRepo.GetAll()).TimeSlotListConv();
        }
    }
}
