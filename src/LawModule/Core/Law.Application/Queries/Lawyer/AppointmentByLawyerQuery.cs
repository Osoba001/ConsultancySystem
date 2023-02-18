using Law.Application.Helpers;
using Law.Application.Response;
using Law.Domain.Repositories;
using SimpleMediatR.MediatRContract;

namespace Law.Application.Queries.Lawyer
{
    public record AppointmentByLawyerQuery(Guid LawyerId) : IQuery;

    public class AppointmentByLawyerHandler : QueryHandler<AppointmentByLawyerQuery>
    {
        public override async Task<object> HandlerAsync(AppointmentByLawyerQuery query, IRepoWrapper repo, CancellationToken cancellationToken = default)
        {
            return (await repo.AppointmentRepo.FindByPredicate(x => x.Lawyer.Id == query.LawyerId)).AppointmentListConv();
        }
    }
}
