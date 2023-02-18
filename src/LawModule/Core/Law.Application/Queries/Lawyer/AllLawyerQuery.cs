using Law.Application.Helpers;
using Law.Application.Response;
using Law.Domain.Repositories;
using SimpleMediatR.MediatRContract;

namespace Law.Application.Queries.Lawyer
{
    public record AllLawyerQuery : IQuery;

    public class AllLawyerHandler : QueryHandler<AllLawyerQuery>
    {
        public override async Task<object> HandlerAsync(AllLawyerQuery query, IRepoWrapper repo, CancellationToken cancellationToken = default)
        {
            return (await repo.LawyerRepo.GetAll()).LawyerListConv();
            ;
        }
    }
}
