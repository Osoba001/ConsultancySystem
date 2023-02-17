using Law.Domain.Models;
using System.Linq.Expressions;
using Utilities.ActionResponse;

namespace Law.Domain.Repositories
{
    public interface IBaseRepo<T> where T : ModelBase
    {
        Task<ActionResult<T>> AddAndReturn(T entity);
        Task<ActionResult<T>> UpdateAndReturn(T entity);
        Task<ActionResult<T>> DeleteAndReturn(Guid id);
        Task<ActionResult> Add(T entity);
        Task<ActionResult> AddRange(IEnumerable<T> entities);
        Task<ActionResult> Update(T entity);
        Task<ActionResult> Delete(Guid id);
        Task<T?> GetById(Guid id, bool IsEagerLoad = false);
        Task<List<T>> FindByPredicate(Expression<Func<T, bool>> predicate, bool IsEagerLoad = false);
        Task<T?> FindOneByPredicate(Expression<Func<T, bool>> predicate, bool IsEagerLoad = false);
        Task<List<T>> GetAll();
        ActionResult FailedAction(string message);
        ActionResult<U> FailedAction<U>(string message) where U : class;
    }
}
