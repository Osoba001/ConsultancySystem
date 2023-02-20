using Law.Domain.Models;
using Law.Domain.Repositories;
using Law.Persistence.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Law.Persistence.Repositories
{
    public class DepartmentRepo : BaseRepo<Department>, IDepartmentRepo
    {
        private readonly LawDbContext _context;

        public DepartmentRepo(LawDbContext context) : base(context)
        {
            _context = context;
        }

        public List<Department> Convertlist(List<Department> listTB)
        {
            var res = new List<Department>();
            foreach (var item in listTB)
            {
                res.Add(item);
            }
            return res;
        }

        public override async Task<List<Department>> FindByPredicate(Expression<Func<Department, bool>> predicate, bool IsEagerLoad = false)
        {
            if (!IsEagerLoad)
            {
                return await _context.DepartmentTB.Where(predicate)
                    .Include(x => x.Lawyers)
                    .ToListAsync();
            }
            return await base.FindByPredicate(predicate, IsEagerLoad);
        }
        public override async Task<Department?> FindOneByPredicate(Expression<Func<Department, bool>> predicate, bool IsEagerLoad = false)
        {
            if (!IsEagerLoad)
            {
                return await _context.DepartmentTB
                    .Where(predicate)
                    .Include(x => x.Lawyers)
                    .FirstOrDefaultAsync();
            }
            return await base.FindOneByPredicate(predicate, IsEagerLoad);
        }
        public override async Task<Department?> GetById(Guid id, bool IsEagerLoad = false)
        {
            if (!IsEagerLoad)
            {
                return await _context.DepartmentTB
                    .Where(x => x.Id == id)
                    .Include(x => x.Lawyers)
                    .FirstOrDefaultAsync();
            }
            return await base.GetById(id, IsEagerLoad);
        }
    }
}
