using Law.Application.Response;
using Law.Domain.Repositories;
using SimpleMediatR.MediatRContract;
using Utilities.ActionResponse;

namespace Law.Application.Queries.Department
{
    public record DepartmentByIdQuery : IQuery
    {
        public Guid Id { get; set; }
    }

    public record DepartmentByIdHandler : IQueryHandler<DepartmentByIdQuery>
    {
        public async Task<ActionResult> HandlerAsync(DepartmentByIdQuery query, IRepoWrapper repo, CancellationToken cancellationToken = default)
        {
            ActionResult resp = new();
            var res = await repo.DepartmentRepo.GetById(query.Id);
            if (res == null)
                resp.AddError("Record is not found.");
            else
            {
                DepartmentResponse r = res;
                resp.data = r;
            }
            return resp;
        }
    }
}
