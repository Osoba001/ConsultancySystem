using LCS.Domain.Models;
using LCS.Domain.Repositories;
using SimpleMediatR.MediatRContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LCS.Application.Queries.AppointmentQ
{
    public record AppointmentByClientQuery(Guid ClientId):IQuery<List<Appointment>>;

    public class AppointmentByClientHandler : IQueryHandler<AppointmentByClientQuery, List<Appointment>>
    {
        public async Task<List<Appointment>> HandlerAsync(AppointmentByClientQuery query, IRepoWrapper repo, CancellationToken cancellationToken = default)
        {
            return repo.AppointmentRepo.Convertlist(await repo.AppointmentRepo.FindByPredicate(x => x.Client.Id == query.ClientId));
        }
    }

   
}
