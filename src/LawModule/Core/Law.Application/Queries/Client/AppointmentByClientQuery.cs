using Law.Application.Helpers;
using Law.Application.Response;
using Law.Domain.Repositories;
using SimpleMediatR.MediatRContract;

namespace Law.Application.Queries.Client
{
    public record AppointmentByClientQuery: IQuery
    {
        public Guid ClientId { get; set; }
    }

    public class AppointmentByClientHandler : QueryHandler<AppointmentByClientQuery>
    {
        public override async Task<object> HandlerAsync(AppointmentByClientQuery query, IRepoWrapper repo, CancellationToken cancellationToken = default)
        {
            return (await repo.AppointmentRepo.FindByPredicate(x => x.Client.Id == query.ClientId)).AppointmentListConv();
        }
    }


}
