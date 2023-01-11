using LCS.Domain.Entities;
using LCS.Domain.Repositories;
using LCS.Domain.Response;
using LCS.Persistence.Data;
using Microsoft.EntityFrameworkCore;
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
        private readonly LCSDbContext _context;

        public BaseRepo(LCSDbContext context)
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

        public async Task<ActionResult> Delete(Guid id)
        {
            var entity= GetById(id);
            if (entity != null)
            {
                _context.Remove(entity);
                return await _context.SaveActionAsync();
            }
            else
            {
                var res = new ActionResult();
                res.AddError("Record not found.");
                return res;
            }
            
        }

        public async Task<ActionResult<T>> DeleteAndReturn(Guid id)
        {
            var entity = await GetById(id);
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

        public async Task<List<T>> FindByPredicate(Expression<Func<T, bool>> predicate)
        {
            return await _context.Set<T>().Where(predicate).ToListAsync();
        }

        public async Task<List<T>> GetAll()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<T?> GetById(Guid id)
        {
            return await _context.Set<T>().Where(x=>x.Id==id).FirstOrDefaultAsync();
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

    }
}
