using LCS.Domain.Entities;
using LCS.Domain.Models;
using LCS.Domain.Repositories;
using LCS.Domain.Response;
using LCS.Persistence.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace LCS.Persistence.Repositories
{
    public class LawyerRepo : BaseRepo<LawyerTB>, ILawyerRepo
    {
        private readonly LCSDbContext _context;

        public LawyerRepo(LCSDbContext context) : base(context)
        {
            _context = context;
        }

        public override async Task<ActionResult> Add(LawyerTB entity)
        {
            var res = new ActionResult();
            IEntityType metadata = _context.Model.FindEntityType(typeof(LawyerTB).FullName!)!;
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
            return  res; 
        }

        public async Task<ActionResult> JoinDepartment(Guid lawyerId, Guid departmentId)
        {
            var lawyer = await _context.LawyerTB.FindAsync(lawyerId);
            if (lawyer != null)
            {
                var dept = await _context.DepartmentTB.FindAsync(departmentId);
                if (dept != null)
                {
                    var lawyerDept = new LawyerDepartmentTB() { Department = dept, Lawyer = lawyer };
                    _context.LawyerDepartmentTB.Add(lawyerDept);
                    return await _context.SaveActionAsync();
                }
                else
                    return FailedAction("Department not found!");
            }
            else
                return FailedAction("User not found!");
        }

        public async Task<ActionResult> LeaveDepartment(Guid lawyerDeptId)
        {
            var lawyerDept = await _context.LawyerDepartmentTB.FindAsync(lawyerDeptId);
            if (lawyerDept is not null)
            {
                _context.LawyerDepartmentTB.Remove(lawyerDept);
                return await _context.SaveActionAsync();
            }
            else
                return FailedAction("Record not found!");
        }

        public async Task<ActionResult> AddLanguage(Guid lawyerId, Guid LanguageId)
        {
            var lawyer = await _context.LawyerTB.FindAsync(lawyerId);
            if(lawyer is not null)
            {
                var lang = await _context.LanguageTB.FindAsync(LanguageId);
                if (lang != null)
                {
                    var LawLang = new LawyerLanguageTB() { Language = lang, Lawyer = lawyer };
                    _context.LawyerLanguageTB.Add(LawLang);
                    return await _context.SaveActionAsync();
                }
                else
                    return FailedAction("Language is not found!");
            }
            else
                return FailedAction("User is not found!");
        }
        public async Task<ActionResult> RemoveLanguage(Guid joinedLanguageId)
        {
            var lawyerLang = await _context.LawyerLanguageTB.FindAsync(joinedLanguageId);
            if (lawyerLang is not null)
            {
                _context.LawyerLanguageTB.Remove(lawyerLang);
                return await _context.SaveActionAsync();
            }
            else
                return FailedAction("Record not found!");
        }

        public async Task<ActionResult> AddSWorkingSlots(Guid lawyerId, List<Guid> slotIds)
        {
            
            var lawyer = await _context.LawyerTB.FindAsync(lawyerId);
            if (lawyer is not null)
            {
                var Allslots = _context.TimeSlotTB;
                var mySlots = Allslots.IntersectBy(slotIds, p => p.Id);
                var workingSlots = new List<WorkingSlotTB>();
                foreach (var s in mySlots)
                {

                    workingSlots.Add(new WorkingSlotTB() { Lawyer = lawyer, TimeSlot = s });
                }
                _context.WorkingSlot.AddRange(workingSlots);
                return await _context.SaveActionAsync();
            }
            else
                return FailedAction("User is not found!");
        }

        public async Task<ActionResult> RemoveWorkingSlots(List<Guid> WorkingSlotIds)
        {
            var ws= _context.WorkingSlot;
            var deletingWs=ws.IntersectBy(WorkingSlotIds, p => p.Id);
            _context.RemoveRange(deletingWs);
            return await _context.SaveActionAsync();
        }
        public List<Lawyer> Convertlist(List<LawyerTB> listTB)
        {
            var res = new List<Lawyer>();
            foreach (var item in listTB)
            {
                res.Add(item);
            }
            return res;
        }

        public async Task FalseDelete(Guid id)
        {
            var user = await _context.LawyerTB.FindAsync(id);
            if (user != null)
            {
                user.IsDelete = true;
                _context.LawyerTB.Update(user);
                await _context.SaveActionAsync();
            }
        }
        public async Task UndoFalseDelete(Guid id)
        {
            var user = await _context.LawyerTB.FindAsync(id);
            if (user != null)
            {
                user.IsDelete = false;
                _context.LawyerTB.Update(user);
                await _context.SaveActionAsync();
            }
        }

        public override async Task<List<LawyerTB>> FindByPredicate(Expression<Func<LawyerTB, bool>> predicate, bool IsEagerLoad = false)
        {
            if (IsEagerLoad)
            {
                return await base.FindByPredicate(predicate, IsEagerLoad);
            }else
                return await _context.LawyerTB.Where(predicate)
                    .Include(x=>x.Languages).ThenInclude(y=>y.Language)
                    .Include(x=>x.Appointments)
                    .Include(x=>x.JoinedDepartments).ThenInclude(y=>y.Department)
                    .Include(x=>x.WorkingSlot).ThenInclude(y=>y.TimeSlot)
                    .ToListAsync();
        }
        public override async Task<LawyerTB?> FindOneByPredicate(Expression<Func<LawyerTB, bool>> predicate, bool IsEagerLoad = false)
        {
            if (!IsEagerLoad)
            {
                return await base.FindOneByPredicate(predicate, IsEagerLoad);
            }
            else
                return await _context.LawyerTB.Where(predicate)
                   .Include(x => x.Languages).ThenInclude(y => y.Language)
                   .Include(x => x.Appointments)
                   .Include(x => x.JoinedDepartments).ThenInclude(y => y.Department)
                   .Include(x => x.WorkingSlot).ThenInclude(y => y.TimeSlot)
                   .FirstOrDefaultAsync(); 
        }
        public async override Task<LawyerTB?> GetById(Guid id, bool IsEagerLoad = false)
        {
            if (!IsEagerLoad)
            {
                return await base.GetById(id, IsEagerLoad);
            }else
                return await _context.LawyerTB.Where(x=>x.Id==id)
                   .Include(x => x.Languages).ThenInclude(y => y.Language)
                   .Include(x => x.Appointments)
                   .Include(x => x.JoinedDepartments).ThenInclude(y => y.Department)
                   .Include(x => x.WorkingSlot).ThenInclude(y => y.TimeSlot)
                   .FirstOrDefaultAsync();
        }

    }
}
