using System.Linq.Expressions;
using User.Application.Entities;

namespace User.Application.Repository
{
    public interface IUserRepo : IBaseRepo<UserTb>
    {
        Task<List<UserTb>> IgnorQueryFilter(Expression<Func<UserTb, bool>> predicate);

    }
}
