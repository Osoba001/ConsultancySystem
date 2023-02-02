using Auth.Data;
using Auth.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Repository
{
    internal class UserRepo : BaseRepo<UserTb>, IUserRepo
    {
        private readonly AuthDbContext _context;

        public UserRepo(AuthDbContext context) : base(context)
        {
            _context = context;
        }

        public override async Task<List<UserTb>> FindByPredicate(Expression<Func<UserTb, bool>> predicate)
        {
            return await _context.UserTb.Where(predicate)
                .Include(x=>x.AssignedUserRoles)
                .ThenInclude(x=>x.Role)
                .ToListAsync();
        }
        public override async Task<UserTb?> FindOneByPredicate(Expression<Func<UserTb, bool>> predicate)
        {
            return await _context.UserTb.Where(predicate)
               .Include(x => x.AssignedUserRoles)
                .ThenInclude(x => x.Role)
               .FirstOrDefaultAsync();
        }

        public async Task<List<UserTb>> IgnorQueryFilter(Expression<Func<UserTb, bool>> predicate)
        {
            return await _context.UserTb
                .IgnoreQueryFilters()
                .Where(predicate)
                .Include(x => x.AssignedUserRoles)
                .ThenInclude(x => x.Role)
                .ToListAsync();
        }

        public override async Task<UserTb?> FindById(Guid id)
        {
           return await _context.UserTb
                .IgnoreQueryFilters()
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();
        }

      
    }
}
