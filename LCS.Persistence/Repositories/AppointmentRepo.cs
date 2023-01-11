using LCS.Domain.Entities;
using LCS.Domain.Repositories;
using LCS.Persistence.Data;

namespace LCS.Persistence.Repositories
{
    public class AppointmentRepo : BaseRepo<AppointmentTB>, IAppointmentRepo
    {
        public AppointmentRepo(LCSDbContext context) : base(context)
        {
        }
    }
}
