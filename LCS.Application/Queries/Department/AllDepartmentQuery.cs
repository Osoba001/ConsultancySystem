using Law.Application.Helpers;
using Law.Application.Response;
using Law.Domain.Repositories;
using SimpleMediatR.MediatRContract;

namespace Law.Application.Queries.Department
{
    public record AllDepartmentQuery : IQuery<List<DepartmentResponse>>;

    public class AllDepartmentHandler : IQueryHandler<AllDepartmentQuery, List<DepartmentResponse>>
    {
        public async Task<List<DepartmentResponse>> HandlerAsync(AllDepartmentQuery query, IRepoWrapper repo, CancellationToken cancellationToken = default)
        {
            return (await repo.DepartmentRepo.GetAll()).DepartmentListConv();
        }


    }
}
