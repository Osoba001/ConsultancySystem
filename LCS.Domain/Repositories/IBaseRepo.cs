using LCS.Domain.Entities;
using LCS.Domain.Models;
using LCS.Domain.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace LCS.Domain.Repositories
{
    public interface IBaseRepo<T> where T : EntityBase
    {
        Task<ActionResult> Add(T entity);
        Task<ActionResult> Update(T entity);
        Task Delete(Guid id);
        Task<T?> GetById(Guid id);
        Task<List<T>> FindByPredicate(Expression<Func<T, bool>> predicate);
        Task<List<T>> GetAll();
    }
}
