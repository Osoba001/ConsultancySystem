using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using User.Application.Entities;
using User.Application.Repository;
using User.Persistence.Data;

namespace User.Persistence.Repositories
{
    internal class UserRepo : BaseRepo<UserTb>, IUserRepo
    {
        private readonly AuthDbContext _context;

        public UserRepo(AuthDbContext context) : base(context)
        {
            _context = context;
        }


        public async Task<List<UserTb>> IgnorQueryFilter(Expression<Func<UserTb, bool>> predicate)
        {
            return await _context.UserTb
                .IgnoreQueryFilters()
                .Where(predicate)
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
