using Auth.Entities;
using Auth.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Repository
{
    public interface IBaseRepo<T> where T : EntityBase
    {
        Task<ActionResult<T>> AddAndReturn(T entity);
        Task<ActionResult<T>> UpdateAndReturn(T entity);
        Task<ActionResult<T>> DeleteAndReturn(Guid id);
        Task<ActionResult> Add(T entity);
        Task<ActionResult> Update(T entity);
        Task<ActionResult> Delete(T entity);
        Task<ActionResult> DeleteRange(List<T> entities);
        Task<List<T>> GetAll();
        Task<List<T>> FindByPredicate(Expression<Func<T, bool>> predicate);
        Task<T?> FindOneByPredicate(Expression<Func<T, bool>> predicate);
        Task<T?> FindById(Guid id);
        Task<ActionResult> DeleteRange(List<Guid> ids);
    }
}
