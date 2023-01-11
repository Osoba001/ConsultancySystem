using LCS.Domain.Entities;
using LCS.Domain.Repositories;
using LCS.Domain.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace LCS.Persistence.Repositories
{
    public class BaseRepo<T> : IBaseRepo<T> where T : EntityBase
    {
        public BaseRepo()
        {

        }
        public Task<ActionResult> Add(T entity)
        {
            throw new NotImplementedException();
        }

        public Task Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<List<T>> FindByPredicate(Expression<Func<T, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Task<List<T>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<T?> GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<ActionResult> Update(T entity)
        {
            throw new NotImplementedException();
        }
    }
}
