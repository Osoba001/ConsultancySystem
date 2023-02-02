using Auth.Data;
using Auth.Entities;
using Auth.Response;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Repository
{
    public class BaseRepo<T> : IBaseRepo<T> where T : EntityBase
    {
        private readonly AuthDbContext _context;

        public BaseRepo(AuthDbContext context)
        {
            _context = context;
        }

        public async Task<ActionResult> Add(T entity)
        {
            _context.Add(entity);
            return await _context.SaveActionAsync();
        }

        public async Task<ActionResult<T>> AddAndReturn(T entity)
        {
            _context.Add(entity);
            return await _context.SaveActionAsync(entity);
        }

        public async Task<ActionResult> Delete(T entity)
        {
            _context.Remove(entity);
            return await _context.SaveActionAsync();

        }

        public async Task<ActionResult<T>> DeleteAndReturn(Guid id)
        {
            var entity = await FindOneByPredicate(x => x.Id == id);
            if (entity != null)
            {
                _context.Remove(entity);
                return await _context.SaveActionAsync(entity);
            }
            else
            {
                var res = new ActionResult<T>();
                res.AddError("Record not found.");
                return res;
            }
        }

        public virtual async Task<T?> FindOneByPredicate(Expression<Func<T, bool>> predicate)
        {
            return await _context.Set<T>().Where(predicate).FirstOrDefaultAsync();
        }
        public virtual async Task<List<T>> FindByPredicate(Expression<Func<T, bool>> predicate)
        {
            return await _context.Set<T>().Where(predicate).ToListAsync();
        }


        public async Task<ActionResult> Update(T entity)
        {
            _context.Update(entity);
            return await _context.SaveActionAsync();
        }

        public async Task<ActionResult<T>> UpdateAndReturn(T entity)
        {
            _context.Update(entity);
            return await _context.SaveActionAsync(entity);
        }

        public async Task<List<T>> GetAll()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public virtual async Task<T?> FindById(Guid id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task<ActionResult> DeleteRange(List<T> entities)
        {
            _context.Set<T>().RemoveRange(entities);
            return await _context.SaveActionAsync();
        }

        public  async Task<ActionResult> DeleteRange(List<Guid> ids)
        {
            var allEntities = _context.Set<T>().IgnoreQueryFilters();
            var entities = allEntities.IntersectBy(ids, x => x.Id);
            _context.Set<T>().RemoveRange(entities);
            return await _context.SaveActionAsync();
        }
    }
}
