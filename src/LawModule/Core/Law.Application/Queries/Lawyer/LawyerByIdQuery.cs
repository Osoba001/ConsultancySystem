using Law.Application.Response;
using Law.Domain.Repositories;
using SimpleMediatR.MediatRContract;
using Utilities.ActionResponse;

namespace Law.Application.Queries.Lawyer
{
    public record LawyerByIdQuery : IQuery
    {
        public Guid Id { get; set; }
    }

    public class LawyerByIdQueryHandler : IQueryHandler<LawyerByIdQuery>
    {
        public async Task<ActionResult> HandlerAsync(LawyerByIdQuery query, IRepoWrapper repo, CancellationToken cancellationToken = default)
        {
            ActionResult resp = new();
            var lawyer = await repo.LawyerRepo.GetById(query.Id);
            if (lawyer == null)
                resp.AddError("Record is not found.");
            else
            {
                LawyerResponse res = lawyer;
                resp.data = res;
            }
            return resp;

        }
    }
}
