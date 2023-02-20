using Law.Application.Helpers;
using Law.Application.Response;
using Law.Domain.Repositories;
using SimpleMediatR.MediatRContract;

namespace Law.Application.Queries.Lawyer
{
    public record LawyerByDepartmentQuery: IQuery
    {
        public Guid DepartmentId { get; set; }
    }

    public class LawyerByDepartmentHandler : QueryHandler<LawyerByDepartmentQuery>
    {
        public override async Task<object> HandlerAsync(LawyerByDepartmentQuery query, IRepoWrapper repo, CancellationToken cancellationToken = default)
        {
            var dept = await repo.DepartmentRepo.GetById(query.DepartmentId);
            if (dept is null)
                return new List<LawyerResponse>();
            return dept.Lawyers.LawyerListConv();

        }
    }
}
