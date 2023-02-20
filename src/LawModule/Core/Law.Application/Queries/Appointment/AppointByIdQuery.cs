using Law.Application.Response;
using Law.Domain.Repositories;
using SimpleMediatR.MediatRContract;
using Utilities.ActionResponse;

namespace Law.Application.Queries.Appointment
{
    public record AppointByIdQuery: IQuery
    {
        public Guid Id { get; set; }
    }

    public record AppointByIdHandler : IQueryHandler<AppointByIdQuery>
    {
        public async Task<ActionResult> HandlerAsync(AppointByIdQuery query, IRepoWrapper repo, CancellationToken cancellationToken = default)
        {
            var resp = new ActionResult();
            var res = await repo.AppointmentRepo.GetById(query.Id);
            if (res == null)
                resp.AddError("Record is not found.");
            else
            {
                AppointmentResponse app = res;
                resp.data = app;
            }
            return resp;
        }
    }
}
