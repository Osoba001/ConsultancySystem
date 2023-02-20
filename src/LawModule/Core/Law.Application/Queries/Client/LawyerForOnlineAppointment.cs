using Law.Application.Helpers;
using Law.Application.Response;
using Law.Domain.Repositories;
using SimpleMediatR.MediatRContract;

namespace Law.Application.Queries.Client
{
    public record LawyerForOnlineAppointment : IQuery
    {
        public string Languge { get; set; }
    }

    public class LawyerForOnlineAppointmentHandler : QueryHandler<LawyerForOnlineAppointment>
    {
        public override async Task<object> HandlerAsync(LawyerForOnlineAppointment query, IRepoWrapper repo, CancellationToken cancellationToken = default)
        {
            return (await repo.LawyerRepo.FindByPredicate(x => x.OnlineWorkingSlots.Any() && x.Languages.Contains(query.Languge))).LawyerListConv();
        }
    }

    
}