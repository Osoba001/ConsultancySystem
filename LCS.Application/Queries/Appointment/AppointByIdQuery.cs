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
    public record AppointByIdQuery(Guid Id):IQuery<Appointment?>;

    public record AppointByIdHandler : IQueryHandler<AppointByIdQuery, Appointment?>
    {
        public async Task<Appointment?> HandlerAsync(AppointByIdQuery query, IRepoWrapper repo, CancellationToken cancellationToken = default)
        {
            var res= await repo.AppointmentRepo.GetById(query.Id);
            if(res == null) return null;
            Appointment appointment = res;
            return appointment;
        }
    }
}
