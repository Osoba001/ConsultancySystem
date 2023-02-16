using Law.Application.Helpers;
using Law.Application.Response;
using Law.Domain.Repositories;
using SimpleMediatR.MediatRContract;

namespace Law.Application.Queries.Lawyer
{
    public record AppointmentByLawyerQuery(Guid LawyerId) : IQuery<List<AppointmentResponse>>;

    public class AppointmentByLawyerHandler : IQueryHandler<AppointmentByLawyerQuery, List<AppointmentResponse>>
    {
        public async Task<List<AppointmentResponse>> HandlerAsync(AppointmentByLawyerQuery query, IRepoWrapper repo, CancellationToken cancellationToken = default)
        {
            return (await repo.AppointmentRepo.FindByPredicate(x => x.Lawyer.Id == query.LawyerId)).AppointmentListConv();
        }
    }
}
