using Law.Domain.Models;
using Law.Domain.Repositories;
using Law.Persistence.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Law.Persistence.Repositories
{
    public class AppointmentRepo : BaseRepo<Appointment>, IAppointmentRepo
    {
        private readonly LawDbContext _context;

        public AppointmentRepo(LawDbContext context) : base(context)
        {
            _context = context;
        }
        public override async Task<List<Appointment>> FindByPredicate(Expression<Func<Appointment, bool>> predicate, bool IsEagerLoad = false)
        {
            if (!IsEagerLoad)
                return await _context.AppointmentTB
                    .Where(predicate)
                    .Include(x => x.Lawyer)
                    .Include(x => x.Client)
                    .Include(x => x.TimeSlot)
                    .Include(x => x.Language)
                    .ToListAsync();

            return await base.FindByPredicate(predicate, IsEagerLoad);
        }
        public async override Task<Appointment?> FindOneByPredicate(Expression<Func<Appointment, bool>> predicate, bool IsEagerLoad = false)
        {
            if (!IsEagerLoad)
                return await _context.AppointmentTB
                    .Where(predicate)
                    .Include(x => x.Lawyer)
                    .Include(x => x.Client)
                    .Include(x => x.TimeSlot)
                    .Include(x => x.Language)
                    .FirstOrDefaultAsync();
            return await base.FindOneByPredicate(predicate, IsEagerLoad);
        }
        public async override Task<Appointment?> GetById(Guid id, bool IsEagerLoad = false)
        {
            if (!IsEagerLoad)
                return await _context.AppointmentTB
                    .Include(x => x.Lawyer)
                    .Include(x => x.Client)
                    .Include(x => x.TimeSlot)
                    .Include(x => x.Language)
                    .FirstAsync();
            return await base.GetById(id, IsEagerLoad);
        }
    }
}
