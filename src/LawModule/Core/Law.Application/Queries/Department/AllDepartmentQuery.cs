using Law.Application.Helpers;
using Law.Application.Response;
using Law.Domain.Repositories;
using SimpleMediatR.MediatRContract;

namespace Law.Application.Queries.Department
{
    public record AllDepartmentQuery : IQuery;

    public class AllDepartmentHandler : QueryHandler<AllDepartmentQuery>
    {
        public override async Task<object> HandlerAsync(AllDepartmentQuery query, IRepoWrapper repo, CancellationToken cancellationToken = default)
        {
            return (await repo.DepartmentRepo.GetAll()).DepartmentListConv();
        }


    }
}
