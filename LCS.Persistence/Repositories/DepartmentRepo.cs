using LCS.Domain.Entities;
using LCS.Domain.Models;
using LCS.Domain.Repositories;
using LCS.Persistence.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq.Expressions;

namespace LCS.Persistence.Repositories
{
    public class DepartmentRepo : BaseRepo<DepartmentTB>, IDepartmentRepo
    {
        private readonly LCSDbContext _context;

        public DepartmentRepo(LCSDbContext context) : base(context)
        {
            _context = context;
        }

        public List<Department> Convertlist(List<DepartmentTB> listTB)
        {
            var res= new List<Department>();
            foreach (var item in listTB)
            {
                res.Add(item);
            }
            return res;
        }

        public override async Task<List<DepartmentTB>> FindByPredicate(Expression<Func<DepartmentTB, bool>> predicate, bool IsEagerLoad = false)
        {
            if (!IsEagerLoad)
            {
                return await _context.DepartmentTB.Where(predicate)
                    .Include(x=>x.JoinedDepartments).ThenInclude(y=>y.Lawyer)
                    .ToListAsync();
            }
            return await base.FindByPredicate(predicate, IsEagerLoad);
        }
        public override async Task<DepartmentTB?> FindOneByPredicate(Expression<Func<DepartmentTB, bool>> predicate, bool IsEagerLoad = false)
        {
            if (!IsEagerLoad)
            {
                return await _context.DepartmentTB
                    .Where(predicate)
                    .Include(x => x.JoinedDepartments)
                    .ThenInclude(y => y.Lawyer)
                    .FirstOrDefaultAsync();
            }
            return await base.FindOneByPredicate(predicate, IsEagerLoad);
        }
        public override async Task<DepartmentTB?> GetById(Guid id, bool IsEagerLoad = false)
        {
            if (!IsEagerLoad)
            {
                return await _context.DepartmentTB
                    .Where(x=>x.Id==id)
                    .Include(x => x.JoinedDepartments)
                    .ThenInclude(y => y.Lawyer)
                    .FirstOrDefaultAsync();
            }
            return await base.GetById(id, IsEagerLoad);
        }
    }
}
