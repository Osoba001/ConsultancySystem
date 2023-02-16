using System.Linq.Expressions;
using User.Application.Entities;
using Utilities.ActionResponse;

namespace User.Application.Repository
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
