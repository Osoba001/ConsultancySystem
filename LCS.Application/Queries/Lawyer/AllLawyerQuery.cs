using Law.Application.Helpers;
using Law.Application.Response;
using Law.Domain.Repositories;
using SimpleMediatR.MediatRContract;

namespace Law.Application.Queries.Lawyer
{
    public record AllLawyerQuery : IQuery<List<LawyerResponse>>;

    public class AllLawyerHandler : IQueryHandler<AllLawyerQuery, List<LawyerResponse>>
    {
        public async Task<List<LawyerResponse>> HandlerAsync(AllLawyerQuery query, IRepoWrapper repo, CancellationToken cancellationToken = default)
        {
            return (await repo.LawyerRepo.GetAll()).LawyerListConv();
            ;
        }
    }
}
