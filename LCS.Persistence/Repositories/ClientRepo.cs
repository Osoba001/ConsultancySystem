using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Utilities.ActionResponse;
using Law.Domain.Models;
using Law.Domain.Repositories;
using Law.Persistence.Data;

namespace Law.Persistence.Repositories
{
    public class ClientRepo : BaseRepo<Client>, IClientRepo
    {
        private readonly LCSDbContext _context;

        public ClientRepo(LCSDbContext context) : base(context)
        {
            _context = context;
        }

        public override async Task<ActionResult> Add(Client entity)
        {
            var res = new ActionResult();
            IEntityType metadata = _context.Model.FindEntityType(typeof(Client).FullName!)!;
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

        public override async Task<List<Client>> FindByPredicate(Expression<Func<Client, bool>> predicate, bool IsEagerLoad = false)
        {
            if (!IsEagerLoad)
            {
                return await base.FindByPredicate(predicate, IsEagerLoad);
            }
            else
                return await _context.ClientTB.Where(predicate)
                    .Include(x => x.Appointments)
                    .ThenInclude(l => l.Lawyer)
                    .ToListAsync();
        }

        public override async Task<Client?> FindOneByPredicate(Expression<Func<Client, bool>> predicate, bool IsEagerLoad = false)
        {
            if (!IsEagerLoad)
            {
                return await base.FindOneByPredicate(predicate, IsEagerLoad);
            }
            else
                return await _context.ClientTB.Where(predicate)
                    .Include(x => x.Appointments)
                    .ThenInclude(l => l.Lawyer)
                    .FirstOrDefaultAsync();
        }
        public async override Task<Client?> GetById(Guid id, bool IsEagerLoad = false)
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

        public override async Task<ActionResult> Delete(Guid id)
        {
            var user = await _context.ClientTB.IgnoreQueryFilters().Where(x => x.Id == id).FirstOrDefaultAsync();
            if (user != null)
            {
                _context.ClientTB.Remove(user);
                return await _context.SaveActionAsync();
            }
            else
                return FailedAction($"{nameof(id)}, Client you're trying to permanently delete is not found.");
        }
    }
}
