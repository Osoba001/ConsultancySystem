using LCS.Domain.Entities;
using LCS.Domain.Models;
using LCS.Domain.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LCS.Domain.Repositories
{
    public interface ILawyerRepo: IBaseRepo<LawyerTB>
    {
        Task<ActionResult> JoinDepartment(Guid lawyerId, Guid departmentId);
        Task<ActionResult> LeaveDepartment(Guid joinDeptId);
        Task<ActionResult> AddLanguage(Guid lawyerId, Guid LanguageId);
        Task<ActionResult> RemoveLanguage(Guid joinedLanguageId);
        Task<ActionResult> AddSWorkingSlots(Guid lawyerId, List<Guid> slotIds);
        Task<ActionResult> RemoveWorkingSlots(List<Guid> WorkingSlotId);

        List<Lawyer> Convertlist(List<LawyerTB> listTB);
    }
}
