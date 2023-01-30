using LCS.Domain.Entities;
using LCS.Domain.Models;
using LCS.Domain.Repositories;
using LCS.Persistence.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq.Expressions;

namespace LCS.Persistence.Repositories
{
    public class AppointmentRepo : BaseRepo<AppointmentTB>, IAppointmentRepo
    {
        private readonly LCSDbContext _context;

        public AppointmentRepo(LCSDbContext context) : base(context)
        {
            _context = context;
        }
        public override async Task<List<AppointmentTB>> FindByPredicate(Expression<Func<AppointmentTB, bool>> predicate, bool IsEagerLoad = false)
        {
            if(!IsEagerLoad)
                return await _context.AppointmentTB
                    .Where(predicate)
                    .Include(x=>x.Lawyer)
                    .Include(x=>x.Client)
                    .Include(x=>x.TimeSlot)
                    .Include(x=>x.language)
                    .ToListAsync();

            return await base.FindByPredicate(predicate, IsEagerLoad);
        }
        public async override Task<AppointmentTB?> FindOneByPredicate(Expression<Func<AppointmentTB, bool>> predicate, bool IsEagerLoad = false)
        {
            if (!IsEagerLoad)
                return await _context.AppointmentTB
                    .Where(predicate)
                    .Include(x => x.Lawyer)
                    .Include(x => x.Client)
                    .Include(x => x.TimeSlot)
                    .Include(x => x.language)
                    .FirstOrDefaultAsync();
            return await base.FindOneByPredicate(predicate, IsEagerLoad);
        }
        public async override Task<AppointmentTB?> GetById(Guid id, bool IsEagerLoad = false)
        {
            if (!IsEagerLoad)
                return await _context.AppointmentTB
                    .Include(x => x.Lawyer)
                    .Include(x => x.Client)
                    .Include(x => x.TimeSlot)
                    .Include(x => x.language)
                    .FirstAsync();
            return await base.GetById(id, IsEagerLoad);
        }

        public List<Appointment> Convertlist(List<AppointmentTB> listTB)
        {
            var res=new List<Appointment>();
            foreach (var item in listTB)
            {
                res.Add(item);
            }
            return res;
        }
    }
}
