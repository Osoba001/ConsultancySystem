using LCS.Domain.Models;
using LCS.Domain.Repositories;
using SimpleMediatR.MediatRContract;

namespace LCS.Application.Queries.AppointmentQ
{
    public record AppointmentByLawyerQuery(Guid LawyerId) : IQuery<List<Appointment>>;

    public class AppointmentByLawyerHandler : IQueryHandler<AppointmentByLawyerQuery, List<Appointment>>
    {
        public async Task<List<Appointment>> HandlerAsync(AppointmentByLawyerQuery query, IRepoWrapper repo, CancellationToken cancellationToken = default)
        {
            return repo.AppointmentRepo.Convertlist(await repo.AppointmentRepo.FindByPredicate(x => x.Lawyer.Id == query.LawyerId));
        }
    }
}
