using Auth.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Repository
{
    public interface IUserRepo:IBaseRepo<UserTb>
    {
        Task<List<UserTb>> IgnorQueryFilter(Expression<Func<UserTb, bool>> predicate);
        
    }
}
