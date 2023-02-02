using Auth.Data;
using Auth.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Auth.Repository
{
    public class AssignedUserRoleRepo : BaseRepo<AssignedUserRoleTb>, IAssignedUserRoleRepo
    {
        private readonly AuthDbContext _context;

        public AssignedUserRoleRepo(AuthDbContext context) : base(context)
        {
            _context = context;
        }

        public override async Task<List<AssignedUserRoleTb>> FindByPredicate(Expression<Func<AssignedUserRoleTb, bool>> predicate)
        {
            return await _context.AssignedUserRoleTb
                .Where(predicate)
                .Include(x=>x.User)
                .Include(x=>x.Role)
                .ToListAsync();
        }
    }
}
