using Law.Application.Helpers;
using Law.Application.Response;
using Law.Domain.Repositories;
using SimpleMediatR.MediatRContract;

namespace Law.Application.Queries.Client
{
    public record LawyerForOfflineAppointment(string Language, string State, string Location) : IQuery;

    public class LawyerForOfflineAppointmentHandler : QueryHandler<LawyerForOfflineAppointment>
    {
        public override async Task<object> HandlerAsync(LawyerForOfflineAppointment query, IRepoWrapper repo, CancellationToken cancellationToken = default)
        {
            return (await repo.LawyerRepo.FindByPredicate(x => x.OfflineWorkingSlots.Any() && x.Languages.Contains(query.Language) && x.Location == query.Location && x.State == query.State)).LawyerListConv();
        }
    }
}