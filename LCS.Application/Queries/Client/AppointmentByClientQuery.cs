using Law.Application.Helpers;
using Law.Application.Response;
using Law.Domain.Repositories;
using SimpleMediatR.MediatRContract;

namespace Law.Application.Queries.Client
{
    public record AppointmentByClientQuery(Guid ClientId) : IQuery<List<AppointmentResponse>>;

    public class AppointmentByClientHandler : IQueryHandler<AppointmentByClientQuery, List<AppointmentResponse>>
    {
        public async Task<List<AppointmentResponse>> HandlerAsync(AppointmentByClientQuery query, IRepoWrapper repo, CancellationToken cancellationToken = default)
        {
            return (await repo.AppointmentRepo.FindByPredicate(x => x.Client.Id == query.ClientId)).AppointmentListConv();
        }
    }


}
