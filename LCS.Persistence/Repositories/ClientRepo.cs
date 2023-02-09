using LCS.Domain.Entities;
using LCS.Domain.Repositories;
using LCS.Domain.Response;
using LCS.Persistence.Data;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore;
using LCS.Domain.Models;
using System.Linq.Expressions;
using System;

namespace LCS.Persistence.Repositories
{
    public class ClientRepo : BaseRepo<ClientTB>, IClientRepo
    {
        private readonly LCSDbContext _context;

        public ClientRepo(LCSDbContext context) : base(context)
        {
            _context = context;
        }

        public override async Task<ActionResult> Add(ClientTB entity)
        {
            var res = new ActionResult();
            IEntityType metadata = _context.Model.FindEntityType(typeof(ClientTB).FullName!)!;
            var schema = metadata.GetSchema();
            var tableName = metadata.GetTableName();

            var stratagy = _context.Database.CreateExecutionStrategy();
            await stratagy.ExecuteAsync(async () =>
            {
                using var trans = _context.Database.BeginTransaction();
                try
                {
                    await _context.Database.ExecuteSqlRawAsync($"SET IDENTITY_INSERT {schema}.{tableName} ON");
                    res = await base.Add(entity);
                }
                catch (Exception ex)
                {
                    await trans.RollbackAsync();
                    res = FailedAction(ex.Message);
                }
                finally
                {
                    await _context.Database.ExecuteSqlRawAsync($"SET IDENTITY_INSERT {schema}.{tableName} OFF");
                }
            });
            return res;
        }

        public List<Client> Convertlist(List<ClientTB> listTB)
        {
            var res=new List<Client>();
            foreach (var item in listTB)
            {
                res.Add(item);
            }
            return res;
        }

        public async Task FalseDelete(Guid id)
        {
            var user=await _context.ClientTB.FindAsync(id);
            if (user != null)
            {
                user.IsDelete= true;
                _context.ClientTB.Update(user);
               await _context.SaveActionAsync();
            }
        }
        public async Task UndoFalseDelete(Guid id)
        {
            var user = await _context.ClientTB.FindAsync(id);
            if (user != null)
            {
                user.IsDelete = false;
                _context.ClientTB.Update(user);
                await _context.SaveActionAsync();
            }
        }

        public override async Task<List<ClientTB>> FindByPredicate(Expression<Func<ClientTB, bool>> predicate, bool IsEagerLoad = false)
        {
            if (!IsEagerLoad)
            {
                return await base.FindByPredicate(predicate, IsEagerLoad);
            } else
                return await _context.ClientTB.Where(predicate)
                    .Include(x => x.Appointments)
                    .ThenInclude(l => l.Lawyer)
                    .ToListAsync();
        }

        public override async Task<ClientTB?> FindOneByPredicate(Expression<Func<ClientTB, bool>> predicate, bool IsEagerLoad = false)
        {
            if (!IsEagerLoad)
            {
                return await base.FindOneByPredicate(predicate, IsEagerLoad);
            }else
                return await _context.ClientTB.Where(predicate)
                    .Include(x => x.Appointments)
                    .ThenInclude(l => l.Lawyer)
                    .FirstOrDefaultAsync();
        }
        public async override Task<ClientTB?> GetById(Guid id, bool IsEagerLoad = false)
        {
            if (!IsEagerLoad)
            {
                return await base.GetById(id, IsEagerLoad);
            }
            else
                return await _context.ClientTB.Where(x => x.Id == id)
                    .Include(x => x.Appointments)
                    .ThenInclude(l => l.Lawyer)
                    .FirstOrDefaultAsync();
        }
    }
}
