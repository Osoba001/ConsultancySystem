using Law.Domain.Models;
using Law.Domain.Repositories;
using Law.Persistence.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System.Linq.Expressions;
using Utilities.ActionResponse;

namespace Law.Persistence.Repositories
{
    public class LawyerRepo : BaseRepo<Lawyer>, ILawyerRepo
    {
        private readonly LawDbContext _context;

        public LawyerRepo(LawDbContext context) : base(context)
        {
            _context = context;
        }

        //public override async Task<ActionResult> Add(Lawyer entity)
        //{
        //    var res = new ActionResult();
        //    IEntityType metadata = _context.Model.FindEntityType(typeof(Lawyer).FullName!)!;
        //    var schema = metadata.GetSchema();
        //    var tableName = metadata.GetTableName();

        //    var stratagy = _context.Database.CreateExecutionStrategy();
        //    await stratagy.ExecuteAsync(async () =>
        //    {
        //        using var trans = _context.Database.BeginTransaction();
        //        try
        //        {
        //            await _context.Database.ExecuteSqlRawAsync($"SET IDENTITY_INSERT {schema}.{tableName} ON");
        //            res = await base.Add(entity);
        //        }
        //        catch (Exception ex)
        //        {
        //            await trans.RollbackAsync();
        //            res = FailedAction(ex.Message);
        //        }
        //        finally
        //        {
        //            await _context.Database.ExecuteSqlRawAsync($"SET IDENTITY_INSERT {schema}.{tableName} OFF");
        //        }
        //    });
        //    return res;
        //}



        public override async Task<List<Lawyer>> FindByPredicate(Expression<Func<Lawyer, bool>> predicate, bool IsEagerLoad = false)
        {
            if (IsEagerLoad)
            {
                return await base.FindByPredicate(predicate, IsEagerLoad);
            }
            else
                return await _context.LawyerTB.Where(predicate)
                    .Include(x => x.Languages)
                    .Include(x => x.Appointments)
                    .Include(x => x.Departments)
                    .Include(x => x.OnlineWorkingSlots)
                    .ToListAsync();
        }
        public override async Task<Lawyer?> FindOneByPredicate(Expression<Func<Lawyer, bool>> predicate, bool IsEagerLoad = false)
        {
            if (!IsEagerLoad)
            {
                return await base.FindOneByPredicate(predicate, IsEagerLoad);
            }
            else
                return await _context.LawyerTB.Where(predicate)
                   .Include(x => x.Languages)
                   .Include(x => x.Appointments)
                   .Include(x => x.Departments)
                   .Include(x => x.OnlineWorkingSlots)
                   .FirstOrDefaultAsync();
        }
        public async override Task<Lawyer?> GetById(Guid id, bool IsEagerLoad = false)
        {
            if (!IsEagerLoad)
            {
                return await base.GetById(id, IsEagerLoad);
            }
            else
                return await _context.LawyerTB.Where(x => x.Id == id)
                   .Include(x => x.Languages)
                   .Include(x => x.Appointments)
                   .Include(x => x.Departments)
                   .Include(x => x.OnlineWorkingSlots)
                   .FirstOrDefaultAsync();
        }

        public override async Task<ActionResult> Delete(Guid id)
        {
            var user = await _context.LawyerTB.IgnoreQueryFilters().Where(x => x.Id == id).FirstOrDefaultAsync();
            if (user != null)
            {
                _context.LawyerTB.Remove(user);
                return await _context.SaveActionAsync();
            }
            else
                return FailedAction($"{nameof(id)}, Lawyer you're trying to permamently delete is not found.");
        }

    }
}
